using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchSupplierPaymentViewModel : Services.Navigation.ViewModel
    {
        private string employeeName;
        public string EmployeeName
        {
            get => employeeName;
            set { employeeName = value; OnPropertyChanged(); }
        }
        
        private Employee _actualEmployee = new Employee();
        private Employee? _originalEmployee;
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

        public ICommand NavigateToSearchEmployeeView { get; set; }
        public ICommand EditEmployeeCommand { get; set; }

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDialogService _dialogService;


        public SearchSupplierPaymentViewModel(INavigationService navigationService, UserService currentUser, IEmployeeRepository employeeRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _employeeRepository = employeeRepository;
            InicializateOptionsList();
            NavigateToSearchEmployeeView = new RelayCommand(NavigateToSearchEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee);

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
        private void InitProperties()
        {
            EmployeeName = ActualEmployee.Name;
            PaternalSurname = ActualEmployee.PaternalSurname;
            MaternalSurname = ActualEmployee.MaternalSurname;
            Street = ActualEmployee.Street;
            Number = ActualEmployee.Number.ToString();
            CP = ActualEmployee.CP;
            City = ActualEmployee.City;
            Phone = ActualEmployee.Phone;
            Mail = ActualEmployee.Email;
            Curp = ActualEmployee.CURP;
            RFC = ActualEmployee.RFC;
            SelectedOption = ActualEmployee.PositionVendor;
            EmployeeNumber = ActualEmployee.EmployeeNumber;
            Branch = ActualEmployee.Branch;
            Username = ActualEmployee.Username;
            Password = ActualEmployee.Password;
        }
        private void InicializateOptionsList()
        {
            OptionsList = new ObservableCollection<string>
            {
                "Asesor de ventas",
                "Ejecutivo de ventas",
                "Asesor de posventa",
                "Especialista en financiamiento"
            };
        }
        private void NavigateToSearchEmployee()
        {
            var confirmationVM = new ConfirmationViewModel("Cancerlar edición", $"¿Esta seguro que desea cancelar la edición? Se perderán todos los cambios no guardados", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Navigation.NavigateTo<SearchEmployeeViewModel>();
            }
        }
        private void EditEmployee()
        {
            var confirmationVM = new ConfirmationViewModel("Confimracion de registro", $"¿Esta seguro que desea registrar a este nuevo empleado?", Utilities.Enum.ConfirmationIconType.RegisterIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Vendedor employee = new Vendedor
                {
                    nombre = this.EmployeeName,
                    apellidoPaterno = this.PaternalSurname,
                    apellidoMaterno = this.MaternalSurname,
                    calle = this.Street,
                    numero = int.TryParse(this.Number, out int parsedNumber) ? parsedNumber : 0,
                    codigoPostal = this.CP,
                    ciudad = this.City,
                    telefono = this.Phone,
                    correo = this.Mail,
                    CURP = this.Curp,
                    RFC = this.RFC,
                    numeroEmpleado = this.EmployeeNumber,
                    sucursal = this.Branch,
                    nombreUsuario = this.Username,
                    puestoVendedor = this.SelectedOption,
                    idVendedor = ActualEmployee.IdEmployee,
                    Reservas = ActualEmployee.Reservas
                };



                try
                {
                    if (_employeeRepository.Edit(employee))
                    {
                        MessageBox.Show("Empleado editado correctamente");
                        Navigation.NavigateTo<SearchEmployeeViewModel>();
                    }
                    else
                    {
                        MessageBox.Show("Error al editar el empleado");

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al registrar el empleado: " + e.StackTrace);
                }
            }


        }
    }
}