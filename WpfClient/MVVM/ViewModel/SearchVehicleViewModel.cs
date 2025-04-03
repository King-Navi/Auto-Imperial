using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Microsoft.Extensions.DependencyInjection;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.View;
using WpfClient.Resources.ViewCards;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchVehicleViewModel : Services.Navigation.ViewModel
    {
        private IVehicleRepository _vehicleRepository;
        public ObservableCollection<VehicleCardViewModel> VehiclesList { get; set; } = new ObservableCollection<VehicleCardViewModel>();


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
        public IRelayCommand AdvancedFilterCommand { get; set; }

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


        public SearchVehicleViewModel(INavigationService navigationService, UserService currentUser, IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
            Navigation = navigationService;

            SearchCommand = new RelayCommand(
                async o =>
                {
                    VehiclesList.Clear();
                    if (!String.IsNullOrWhiteSpace(SearchText))
                    {
                        var vehicles = await SearchVehiclesAsync();

                        foreach (var newVehicle in vehicles)
                        {
                            VehiclesList.Add(new VehicleCardViewModel(navigationService, newVehicle));
                        }
                    }
                },
                o => !String.IsNullOrWhiteSpace(SearchText));

            Mediator.Register(MediatorKeys.ADVANCED_VEHICLE_SEARCH, args =>
            {
                if (args is AutoImperialDAO.Utilities.VehicleSearch search)
                {
                    AdvancedSearch(search);
                }
            });

            //TODO add filter command


            AdvancedFilterCommand = new RelayCommand(NavigateToRegisterVehicle);
                

        }



        private void NavigateToRegisterVehicle()
        {
            var viewModel = new AdvancedVehicleSearhViewModel();
            var window = new AdvancedVehicleSearhView
            {
                DataContext = viewModel
            };
            window.Show(); 
        }

        private async Task<List<Vehicle>> SearchVehiclesAsync()
        {
            try
            {
                var result = await _vehicleRepository.SearchVehicleAsync(SearchText, VehicleStatus.Disponible);
                if (result == null || result.Count == 0)
                {
                    ErrorMessage = "No se encontraron vehículos con los datos proporcionados.";
                    return new List<Vehicle>();
                }

                ErrorMessage = string.Empty;
                return result.Select(vehiculo => new Vehicle
                {
                    VehicleId = vehiculo.idVehiculo,
                    Branch = vehiculo.idVersionNavigation.idModeloNavigation.idMarcaNavigation.nombre,
                    Model = vehiculo.idVersionNavigation.idModeloNavigation.nombre,
                    Version = vehiculo.idVersionNavigation.nombre,
                    IdBranch = vehiculo.idVersionNavigation.idModeloNavigation.idMarcaNavigation.idMarca,
                    IdModel = vehiculo.idVersionNavigation.idModeloNavigation.idModelo,
                    IdVersion = vehiculo.idVersionNavigation.idVersion,
                    VehicleType = vehiculo.tipoVehiculo,
                    VehicleStatus = vehiculo.estadoVehiculo,
                    SupplierPrice = vehiculo.precioProveedor ?? 0,
                    SellPrice = vehiculo.precioVehiculo ?? 0,
                    Year = vehiculo.anio ?? 0,
                    Color = vehiculo.color,
                    VIN = vehiculo.VIN,
                    ChassisNumber = vehiculo.numeroChasis,
                    EngineNumber = vehiculo.numeroMotor,
                    SupplierPurchaseName = vehiculo.idCompraProveedor,
                    Transmission = vehiculo.idVersionNavigation.transmision,
                    Doors = vehiculo.idVersionNavigation.puertas?.ToString(),
                    Engine = vehiculo.idVersionNavigation.motor,

                    Photos = vehiculo.Fotos.Select(f => new Foto { foto = f.foto }).ToList()

                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la búsqueda de vehículos: {ex.Message}");
                return new List<Vehicle>();
            }
        }

        private void AdvancedSearch(AutoImperialDAO.Utilities.VehicleSearch search)
        {
            VehiclesList.Clear();
            var vehicles = AdvancedSearchVehicles(search);
                foreach (var newVehicle in vehicles)
                {
                    VehiclesList.Add(new VehicleCardViewModel(Navigation, newVehicle));
                }
        }

        private List<Vehicle> AdvancedSearchVehicles(AutoImperialDAO.Utilities.VehicleSearch search)
        {
            search.SearchTerm = SearchText;
            

            try
            {
                var result = _vehicleRepository.AdvancedSearchVehicle(search, VehicleStatus.Disponible);
                if (result == null || result.Count == 0)
                {
                    ErrorMessage = "No se encontraron vehículos con los datos proporcionados.";
                    return new List<Vehicle>();
                }

                ErrorMessage = string.Empty;
                return result.Select(vehiculo => new Vehicle
                {
                    VehicleId = vehiculo.idVehiculo,
                    Branch = vehiculo.idVersionNavigation.idModeloNavigation.idMarcaNavigation.nombre,
                    Model = vehiculo.idVersionNavigation.idModeloNavigation.nombre,
                    Version = vehiculo.idVersionNavigation.nombre,
                    IdBranch = vehiculo.idVersionNavigation.idModeloNavigation.idMarcaNavigation.idMarca,
                    IdModel = vehiculo.idVersionNavigation.idModeloNavigation.idModelo,
                    IdVersion = vehiculo.idVersionNavigation.idVersion,
                    VehicleType = vehiculo.tipoVehiculo,
                    VehicleStatus = vehiculo.estadoVehiculo,
                    SupplierPrice = vehiculo.precioProveedor ?? 0,
                    SellPrice = vehiculo.precioVehiculo ?? 0,
                    Year = vehiculo.anio ?? 0,
                    Color = vehiculo.color,
                    VIN = vehiculo.VIN,
                    ChassisNumber = vehiculo.numeroChasis,
                    EngineNumber = vehiculo.numeroMotor,
                    SupplierPurchaseName = vehiculo.idCompraProveedor,
                    Transmission = vehiculo.idVersionNavigation.transmision,
                    Doors = vehiculo.idVersionNavigation.puertas?.ToString(),
                    Engine = vehiculo.idVersionNavigation.motor,

                    Photos = vehiculo.Fotos.Select(f => new Foto { foto = f.foto }).ToList()

                }).ToList(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la búsqueda avanzada de vehículos: {ex.Message}");
                return new List<Vehicle>();
            }
        }

        public void ResetSearch()
        {
            SearchText = string.Empty;
            ErrorMessage = string.Empty;
            VehiclesList.Clear();
        }

    }
}
