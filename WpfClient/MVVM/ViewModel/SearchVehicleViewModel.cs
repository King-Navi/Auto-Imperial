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
using WpfClient.MVVM.Model;
using WpfClient.MVVM.View;
using WpfClient.Resources.ViewCards;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchVehicleViewModel : Services.Navigation.ViewModel
    {
        //private IVehicleRepository _vehicleRepository;
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


        public SearchVehicleViewModel(INavigationService navigationService, UserService currentUser)//, IVehicleRepository vehicleRepository)
        {
            //_vehicleRepository = vehicleRepository;
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

            //TODO add filter command
            

            AdvancedFilterCommand = new RelayCommand(NavigateToRegisterVehicle);
                

        }

        private void NavigateToRegisterVehicle()
        {
            Supplier supplier = new Supplier();

            RegisterVehicleViewModel viewModel = new RegisterVehicleViewModel(
                App.ServiceProvider.GetRequiredService<INavigationService>(),
                App.ServiceProvider.GetRequiredService<UserService>(),
                App.ServiceProvider.GetRequiredService<IDialogService>(),
                App.ServiceProvider.GetRequiredService<IVehicleRepository>(),
                supplier // ← tu parámetro externo
            );

            var window = new RegisterVehicleView(viewModel);
            window.ShowDialog();
        }

        private async Task<List<Vehicle>> SearchVehiclesAsync()
        {
            try
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                //if (!String.IsNullOrWhiteSpace(SearchText))
                //{
                //    var result = await _vehiclesRepository.SearchByCurpRfcNameAsync(
                //        SearchText, AccountStatusEnum.Activo);
                //    if (result.FirstOrDefault().idVendedor == -1)
                //    {
                //        ErrorMessage = "No se encontraron empleados con los datos proporcionados";
                //    }
                //    else
                //    {
                //        ErrorMessage = string.Empty;
                //        return ConvertToEmployeeList(result);
                //    }
                //}
                var vehicle1 = new Vehicle
                {
                    VehicleId = 1,
                    Branch = "Toyoya",
                    Model = "Rav4",
                    Version = "XLE AWD",
                    VehicleType = "SUV",
                    VehicleStatus = "Available",
                    SupplierPrice = 420000.00m,
                    SellPrice = 459999.99m,
                    Year = 2023,
                    Color = "Pearl White",
                    VIN = "JTMBFREV6JJ123456",
                    ChassisNumber = "CHS2023RAV4XLE01",
                    EngineNumber = "ENGR4XLE2023T",
                    SupplierPurchaseName = 1001,
                    Transmission = "Automatic",
                    Doors = "5",
                    Engine = "2.5L 4-Cylinder"
                };

                var vehicle2 = new Vehicle
                {
                    VehicleId = 2,
                    Branch = "Mazda",
                    Model = "3",
                    Version = "Touring",
                    VehicleType = "Sedan",
                    VehicleStatus = "Sold",
                    SupplierPrice = 320000.00m,
                    SellPrice = 349999.00m,
                    Year = 2021,
                    Color = "Soul Red",
                    VIN = "3MZBPADL0MM123456",
                    ChassisNumber = "CHS2021MZ3TRG02",
                    EngineNumber = "ENGM3TRG2021Z",
                    SupplierPurchaseName = 1002,
                    Transmission = "Manual",
                    Doors = "4",
                    Engine = "2.0L Skyactiv-G"
                };

                vehicles.Add(vehicle1);
                vehicles.Add(vehicle2);
                return vehicles;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la búsqueda de vehículos: {ex.Message}");
            }
            return new List<Vehicle>();
        }

        //private List<SellerEmployee> ConvertToEmployeeList(List<Vendedor> list)
        //{
        //    List<SellerEmployee> employees = new List<SellerEmployee>();

        //    foreach (var emp in list)
        //    {
        //        SellerEmployee newEmployee = new SellerEmployee();
        //        newEmployee.IdEmployee = emp.idVendedor;
        //        newEmployee.CURP = emp.CURP;
        //        newEmployee.CP = emp.codigoPostal;
        //        newEmployee.City = emp.ciudad;
        //        newEmployee.Password = emp.password;
        //        newEmployee.State = emp.estadoCuenta;
        //        newEmployee.Name = emp.nombre;
        //        newEmployee.PaternalSurname = emp.apellidoPaterno;
        //        newEmployee.MaternalSurname = emp.apellidoMaterno;
        //        newEmployee.Phone = emp.telefono;
        //        newEmployee.Email = emp.correo;
        //        newEmployee.Street = emp.calle;
        //        newEmployee.Number = emp.numero;
        //        newEmployee.RFC = emp.RFC;
        //        newEmployee.PositionVendor = emp.puestoVendedor;
        //        newEmployee.Username = emp.nombreUsuario;
        //        newEmployee.EmployeeNumber = emp.numeroEmpleado;
        //        newEmployee.Branch = emp.sucursal;

        //        employees.Add(newEmployee);
        //    }
        //    return employees;
        //}

    }
}
