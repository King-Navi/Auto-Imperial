using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfClient.Idioms;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using WpfClient.Utilities.Enum;

namespace WpfClient.MVVM.ViewModel
{
    class RegisterEmployeeViewModel : Services.Navigation.ViewModel
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
        public ICommand RegisterEmployeeCommand { get; set; }

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDialogService _dialogService;

        public RegisterEmployeeViewModel(INavigationService navigationService, UserService currentUser, IEmployeeRepository employeeRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _employeeRepository = employeeRepository;
            InicializateOptionsList();
            NavigateToSearchEmployeeView = new RelayCommand(NavigateToSearchEmployee);
            RegisterEmployeeCommand = new RelayCommand(RegisterEmployee);

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
            var confirmationVM = new ConfirmationViewModel("Cancerlar registro", $"¿Esta seguro que desea salir del registro? Se perderán todos los cambios no guardados", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Navigation.NavigateTo<SearchEmployeeViewModel>();
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(EmployeeName) ||
                string.IsNullOrWhiteSpace(PaternalSurname) ||
                string.IsNullOrWhiteSpace(MaternalSurname) ||
                string.IsNullOrWhiteSpace(Street) ||
                string.IsNullOrWhiteSpace(Number) ||
                string.IsNullOrWhiteSpace(CP) ||
                string.IsNullOrWhiteSpace(City) ||
                string.IsNullOrWhiteSpace(Phone) ||
                string.IsNullOrWhiteSpace(Mail) ||
                string.IsNullOrWhiteSpace(Curp) ||
                string.IsNullOrWhiteSpace(RFC) ||
                string.IsNullOrWhiteSpace(EmployeeNumber) ||
                string.IsNullOrWhiteSpace(Branch) ||
                string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(SelectedOption))
            {
                _dialogService.ShowDialog(new AlertViewModel("Campos vacíos", "Se han dejado campos vacíos, todos los campos son obligatorios.",AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(Mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un correo no válido", AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(Curp, @"^[A-Z]{4}\d{6}[A-Z]{6}\d{2}$", RegexOptions.IgnoreCase))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un CURP no válido", AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(RFC, @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$", RegexOptions.IgnoreCase))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un RFC no válido", AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(CP, @"^\d{5}$"))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un código postal no válido", AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(Phone, @"^\d{10}$"))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un número de teléfono no válido, debe de tener 10 dígitos", AlertIconType.AlertIcon));
                return false;
            }

            if (!int.TryParse(Number, out _))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un número de calle no válido", AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(Password, @"^(?=.*[A-Z])(?=.*\d).+$"))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "La contraseña debe contener por lo menos una mayúscula y un número", AlertIconType.AlertIcon));
                return false;
            }

            return true;
        }
        private void RegisterEmployee()
        {
            if (!ValidateFields())
                return;

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
                    password = this.Password,
                    puestoVendedor = this.SelectedOption
                };

                try
                {
                    if (_employeeRepository.Register(employee))
                    {
                        MessageBox.Show("Empleado registrado correctamente");
                        Navigation.NavigateTo<SearchEmployeeViewModel>();
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el empleado");
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
