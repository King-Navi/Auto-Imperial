using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Resources.ViewCards;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchSupplierViewModel : Services.Navigation.ViewModel
    {
        private ISupplierRepository _supplierRepository;
        public ObservableCollection<SupplierCardViewModel> SuppliersList { get; set; } = new ObservableCollection<SupplierCardViewModel>();


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

        public SearchSupplierViewModel(INavigationService navigationService, UserService currentUser, ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
            Navigation = navigationService;

            SearchCommand = new RelayCommand(
                async o =>
                {
                    SuppliersList.Clear();
                    if (!String.IsNullOrWhiteSpace(SearchText))
                    {
                        var suppliers = await SearchSuppliersAsync();

                        foreach (var newSupplier in suppliers)
                        {
                            SuppliersList.Add(new SupplierCardViewModel(navigationService, newSupplier));
                        }
                    }
                },
                o => !String.IsNullOrWhiteSpace(SearchText));


            Navigation = navigationService;
            NavigateToRegisterSupplierView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<RegisterSupplierViewModel>();
                },
                o => true);
        }

        private void NavigateToRegisterSupplier()
        {
            Navigation.NavigateTo<RegisterSupplierViewModel>();
        }

        private async Task<List<Supplier>> SearchSuppliersAsync()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(SearchText))
                {
                    var result = await _supplierRepository.SearchByNameCityAsync(
                        SearchText, AccountStatusEnum.Activo);
                    if (result.FirstOrDefault().idProveedor == -1)
                    {
                        ErrorMessage = "No se encontraron empleados con los datos proporcionados";
                    }
                    else
                    {
                        ErrorMessage = string.Empty;
                        return ConvertToEmployeeList(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la búsqueda de empleados: {ex.Message}");
            }
            return new List<Supplier>();
        }

        private List<Supplier> ConvertToEmployeeList(List<Proveedor> list)
        {
            List<Supplier> suppliers = new List<Supplier>();

            foreach (var supp in list)
            {
                Supplier newSupplier = new Supplier();
                newSupplier.SupplierId = supp.idProveedor;
                newSupplier.SupplierName = supp.nombreProveedor;
                newSupplier.City = supp.ciudad;
                newSupplier.Street = supp.calle;
                newSupplier.Number = supp.numero;
                newSupplier.State = supp.estado;
                newSupplier.ZipCode = supp.codigoPostal;
                newSupplier.Phone = supp.telefono;
                newSupplier.Email = supp.correo;
                newSupplier.PrimaryContact = supp.contactoPrincipal;
                newSupplier.ComprasProveedor = supp.ComprasProveedor;

                suppliers.Add(newSupplier);
            }
            return suppliers;
        }
    }
}
