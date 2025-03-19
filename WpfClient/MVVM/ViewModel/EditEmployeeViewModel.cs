using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
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
using Services.Dialogs;

namespace WpfClient.MVVM.ViewModel
{
    class EditEmployeeViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private string employeeName;
        public string EmployeeName
        {
            get => employeeName;
            set { employeeName = value; OnPropertyChanged(); }
        }
        private string paternalSurname;
        public string PaternalSurname
        {
            get => paternalSurname;
            set { paternalSurname = value; OnPropertyChanged(); }
        }
        private string maternalSurname;
        public string MaternalSurname
        {
            get => maternalSurname;
            set { maternalSurname = value; OnPropertyChanged(); }
        }
        private string street;
        public string Street
        {
            get => street;
            set { street = value; OnPropertyChanged(); }
        }
        private string number;
        public string Number
        {
            get => number;
            set { number = value; OnPropertyChanged(); }
        }
        private string cp;
        public string CP
        {
            get => cp;
            set { cp = value; OnPropertyChanged(); }
        }
        private string city;
        public string City
        {
            get => city;
            set { city = value; OnPropertyChanged(); }
        }
        private string phone;
        public string Phone
        {
            get => phone;
            set { phone = value; OnPropertyChanged(); }
        }
        private string mail;
        public string Mail
        {
            get => mail;
            set { mail = value; OnPropertyChanged(); }
        }
        private string curp;
        public string Curp
        {
            get => curp;
            set { curp = value; OnPropertyChanged(); }
        }
        private string rfc;
        public string RFC
        {
            get => rfc;
            set { rfc = value; OnPropertyChanged(); }
        }
        private string employeeNumber;
        public string EmployeeNumber
        {
            get => employeeNumber;
            set { employeeNumber = value; OnPropertyChanged(); }
        }
        private string branch;
        public string Branch
        {
            get => branch;
            set { branch = value; OnPropertyChanged(); }
        }
        private string usernamer;
        public string Username
        {
            get => usernamer;
            set { usernamer = value; OnPropertyChanged(); }
        }
        private string password;
        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> OptionsList { get; set; }

        private string selectedOption;
        public string SelectedOption
        {
            get { return selectedOption; }
            set
            {
                if (selectedOption != value)
                {
                    selectedOption = value;
                    OnPropertyChanged(nameof(SelectedOption));
                }
            }
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


        public EditEmployeeViewModel(INavigationService navigationService, UserService currentUser, IEmployeeRepository employeeRepository, IDialogService dialogService)
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
