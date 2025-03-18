using AutoImperialDAO.DAO.Interfaces;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfClient.MVVM.ViewModel
{
    class InfoEmployeeViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private Employee _actualEmployee = new Employee();
        private Employee? _originalEmployee;

        public string EmployeeName { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string CP { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Curp { get; set; }
        public string RFC { get; set; }
        public string Position { get; set; }
        public string EmployeeNumber { get; set; }
        public string Branch { get; set; }





        public Employee ActualEmployee
        {
            get => _actualEmployee;
            set
            {
                _actualEmployee = value;
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
        //private readonly IClientRepository _clientRepository;


        public ICommand NavigateToSearchEmployeeView { get; set; }
        public InfoEmployeeViewModel(INavigationService navigationService, IDialogService dialogService)//, IClientRepository clientRepository)
        {
            _dialogService = dialogService;
            //_clientRepository = clientRepository; //TODO Employee repository
            Navigation = navigationService;

            
            NavigateToSearchEmployeeView = new RelayCommand(NavigateToSearchEmployee);
        }
        public void ReceiveParameter(object parameter)
        {
            if (parameter is Employee employee)
            {
                ActualEmployee = employee;
                _originalEmployee = (Employee)employee.Clone();

                InitProperties();
            }
            else
            {
                MessageBox.Show("Error al cargar el empleado");
            }
        }

        private void NavigateToSearchEmployee()
        {
            Navigation.NavigateTo<SearchEmployeeViewModel>();
        }

        private void InitProperties()
        {
            EmployeeName = ActualEmployee.Name + " " + ActualEmployee.PaternalSurname + " " + ActualEmployee.MaternalSurname;
            Street = ActualEmployee.Street;
            Number = ActualEmployee.Number.ToString();
            CP = ActualEmployee.CP;
            City = ActualEmployee.City;
            Phone = ActualEmployee.Phone;
            Mail = ActualEmployee.Email;
            Curp = ActualEmployee.CURP;
            RFC = ActualEmployee.RFC;
            Position = ActualEmployee.PositionVendor;
            EmployeeNumber = ActualEmployee.EmployeeNumber;
            Branch = ActualEmployee.Branch;
        }

        


    }
}
