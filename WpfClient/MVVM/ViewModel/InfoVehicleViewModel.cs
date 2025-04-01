using AutoImperialDAO.DAO.Interfaces;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class InfoVehicleViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private Vehicle _actualVehicle = new Vehicle();
        private Vehicle? _originalVehicle;

        public string Branch { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string Color { get; set; }
        public string Transmission { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string VIN { get; set; }
        public string Discount { get; set; }
        public string Year { get; set; }
        public string PurchasePrice { get; set; }
        public string SellPrice { get; set; }
        public string Type { get; set; }
        public string Doors { get; set; }
        public string Engine { get; set; }


        public Vehicle ActualVehicle
        {
            get => _actualVehicle;
            set
            {
                _actualVehicle = value;
                OnPropertyChanged();
            }
        }

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
        private readonly IDialogService _dialogService;
        //private readonly IVehicleRepository _vehicleRepository;


        public ICommand NavigateToSearchVehicleView { get; set; }
        public ICommand DeleteVehicleCommand { get; set; }
        public ICommand EditVehicleCommand { get; set; }
        public InfoVehicleViewModel(INavigationService navigationService, IDialogService dialogService) //, IVehicleRepository vehicleRepository)
        {
            _dialogService = dialogService;
            //_vehicleRepository = vehicleRepository;
            Navigation = navigationService;


            NavigateToSearchVehicleView = new RelayCommand(NavigateToSearchVehicle);
            DeleteVehicleCommand = new RelayCommand(DeleteVehicle);
            EditVehicleCommand = new RelayCommand(EditVehicle);
        }
        public void ReceiveParameter(object parameter)
        {
            if (parameter is Vehicle vehicle)
            {
                ActualVehicle = vehicle;
                //_originalVehicle = (Vehicle)vehicle.Clone();

                InitProperties();
            }
            else
            {
                MessageBox.Show("Error al cargar el vehículo");
            }
        }

        private void NavigateToSearchVehicle()
        {
            Navigation.NavigateTo<SearchVehicleViewModel>();
        }

        private void EditVehicle()
        {
            Navigation.NavigateTo<EditVehicleViewModel>(ActualVehicle);
        }

        void DeleteVehicle() //TODO all method
        {
            var confirmationVM = new ConfirmationViewModel("Confimracion de eliminación", $"¿Desea eliminar este empleado?", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (false == result)
            {
                return;
            }
            //DeleteEmployeeOnDB(ActualVehicle.IdEmployee);
        }


        private void InitProperties()
        {
            Branch = ActualVehicle.Branch;
            Model = ActualVehicle.Model;
            Version = ActualVehicle.Version;
            Color = ActualVehicle.Color;
            Transmission = ActualVehicle.Transmission;
            ChassisNumber = ActualVehicle.ChassisNumber;
            EngineNumber = ActualVehicle.EngineNumber;
            VIN = ActualVehicle.VIN;
            Discount = ActualVehicle.Discounts.FirstOrDefault();
            Year = ActualVehicle.Year.ToString();
            PurchasePrice = ActualVehicle.SupplierPrice.ToString();
            SellPrice = ActualVehicle.SellPrice.ToString();
            Type = ActualVehicle.VehicleType;
            Doors = ActualVehicle.Doors;
            Engine = ActualVehicle.Engine;
        }

        //private void DeleteEmployeeOnDB(int IdEmployee)
        //{
        //    if (_employeeRepository.DeleteById(IdEmployee))
        //    {
        //        MessageBox.Show("Empleado eliminado correctamente");
        //        Navigation.NavigateTo<SearchEmployeeViewModel>();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Error al eliminar el empleado");
        //    }
        //}




    }
}
