using AutoImperialDAO.DAO.Interfaces;
using Services.Dialogs;
using Services.Navigation;
using System.Windows;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class InfoEmployeeViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private SellerEmployee _actualEmployee = new SellerEmployee();
        private SellerEmployee? _originalEmployee;

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





        public SellerEmployee ActualEmployee
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
        private readonly IEmployeeRepository _employeeRepository;


        public ICommand NavigateToSearchEmployeeView { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public InfoEmployeeViewModel(INavigationService navigationService, IDialogService dialogService, IEmployeeRepository employeeRepository)
        {
            _dialogService = dialogService;
            _employeeRepository = employeeRepository;
            Navigation = navigationService;

            
            NavigateToSearchEmployeeView = new RelayCommand(NavigateToSearchEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee);
        }
        public void ReceiveParameter(object parameter)
        {
            if (parameter is SellerEmployee employee)
            {
                ActualEmployee = employee;
                _originalEmployee = (SellerEmployee)employee.Clone();

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

        private void EditEmployee()
        {
            Navigation.NavigateTo<EditEmployeeViewModel>(ActualEmployee);
        }

        void DeleteEmployee()
        {
            var confirmationVM = new ConfirmationViewModel("Confimracion de eliminación", $"¿Desea eliminar este empleado?", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (false == result)
            {
                return;
            }
            DeleteEmployeeOnDB(ActualEmployee.IdEmployee);
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

        private void DeleteEmployeeOnDB(int IdEmployee)
        {
            if (_employeeRepository.DeleteById(IdEmployee))
            {
                MessageBox.Show("Empleado eliminado correctamente");
                Navigation.NavigateTo<SearchEmployeeViewModel>();
            }
            else
            {
                MessageBox.Show("Error al eliminar el empleado");
            }
        }




    }
}
