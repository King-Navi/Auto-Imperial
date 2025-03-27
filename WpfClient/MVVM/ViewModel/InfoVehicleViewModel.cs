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

        //public string EmployeeName { get; set; }
        //public string Street { get; set; }
        //public string Number { get; set; }
        //public string CP { get; set; }
        //public string City { get; set; }
        //public string Phone { get; set; }
        //public string Mail { get; set; }
        //public string Curp { get; set; }
        //public string RFC { get; set; }
        //public string Position { get; set; }
        //public string EmployeeNumber { get; set; }
        //public string Branch { get; set; }





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
        //private readonly IEmployeeRepository _employeeRepository;


        public ICommand NavigateToSearchVehicleView { get; set; }
        public ICommand DeleteVehicleCommand { get; set; }
        public ICommand EditVehicleCommand { get; set; }
        public InfoVehicleViewModel(INavigationService navigationService, IDialogService dialogService) //, IEmployeeRepository employeeRepository)
        {
            _dialogService = dialogService;
            //_employeeRepository = employeeRepository;
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

                //InitProperties();
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
            //Navigation.NavigateTo<EditEmployeeViewModel>(ActualEmployee);
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


        //private void InitProperties()
        //{
        //    EmployeeName = ActualEmployee.Name + " " + ActualEmployee.PaternalSurname + " " + ActualEmployee.MaternalSurname;
        //    Street = ActualEmployee.Street;
        //    Number = ActualEmployee.Number.ToString();
        //    CP = ActualEmployee.CP;
        //    City = ActualEmployee.City;
        //    Phone = ActualEmployee.Phone;
        //    Mail = ActualEmployee.Email;
        //    Curp = ActualEmployee.CURP;
        //    RFC = ActualEmployee.RFC;
        //    Position = ActualEmployee.PositionVendor;
        //    EmployeeNumber = ActualEmployee.EmployeeNumber;
        //    Branch = ActualEmployee.Branch;
        //}

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
