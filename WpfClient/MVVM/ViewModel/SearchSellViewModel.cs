using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.DAO.Repositories;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Resources.ViewCards;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchSellViewModel : Services.Navigation.ViewModel
    {
        private ISellRepository _sellRepository;
        public ObservableCollection<SellCardViewModel> SellsList { get; set; } = new ObservableCollection<SellCardViewModel>();


        private INavigationService navigation;
        public INavigationService Navigation
        {
            get => navigation;
            set
            {
                navigation = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public IRelayCommand SearchCommand { get; set; }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToRegisterSupplierView { get; set; }

        public SearchSellViewModel(INavigationService navigationService, UserService currentUser, ISellRepository sellRepository)
        {
            _sellRepository = sellRepository;
            Navigation = navigationService;

            SearchCommand = new RelayCommand(
                async o =>
                {
                    SellsList.Clear();
                    if (!String.IsNullOrWhiteSpace(SearchText))
                    {
                        var sells = await SearchSellsAsync();

                        foreach (var newSell in sells)
                        {
                            SellsList.Add(new SellCardViewModel(navigationService, newSell));
                        }
                    }
                },
                o => !String.IsNullOrWhiteSpace(SearchText));
        }


        private async Task<List<Sell>> SearchSellsAsync()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(SearchText))
                {
                    var result = await _sellRepository.SearchByVINClientAsync(SearchText);
                    if (result.Count() == 0)
                    {
                        ErrorMessage = "No se encontraron empleados con los datos proporcionados";
                    }
                    else
                    {
                        ErrorMessage = string.Empty;
                        return ConvertToSellList(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la búsqueda de empleados: {ex.Message}");
            }
            return new List<Sell>();
        }

        private List<Sell> ConvertToSellList(List<Venta> list)
        {
            List<Sell> sells = new List<Sell>();

            foreach (var venta in list)
            {
                Sell newSell = new Sell
                {
                    SellId = venta.idVenta,
                    SellDate = venta.fechaVenta,
                    VehiclePrice = venta.precioVehiculo,
                    PaymentMethod = venta.formaPago,
                    AdditionalNotes = venta.notasAdicionales,
                    ReservationId = venta.idReserva,
                    VehicleId = venta.idVehiculo,
                    Reservation = venta.idReservaNavigation,
                    Vehicle = venta.idVehiculoNavigation
                };

                sells.Add(newSell);
            }

            return sells;
        }

    }
}
