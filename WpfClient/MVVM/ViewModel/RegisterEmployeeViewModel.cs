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

        public RegisterEmployeeViewModel(INavigationService navigationService, UserService currentUser)
        {
            Navigation = navigationService;
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
            Navigation.NavigateTo<SearchEmployeeViewModel>();
        }
        private void RegisterEmployee()
        {
            // Crear un nuevo empleado usando las propiedades actuales
            Employee employee = new Employee
            {
                Name = this.EmployeeName,
                PaternalSurname = this.PaternalSurname,
                MaternalSurname = this.MaternalSurname,
                Street = this.Street,
                Number = int.TryParse(this.Number, out int parsedNumber) ? parsedNumber : 0,
                CP = this.CP,
                City = this.City,
                Phone = this.Phone,
                Email = this.Mail,
                CURP = this.Curp,
                RFC = this.RFC,
                EmployeeNumber = this.EmployeeNumber,
                Branch = this.Branch,
                nombreUsuario = this.Username,
                Password = this.Password,
                PositionVendor = this.SelectedOption
            };

            // Construir el mensaje a mostrar
            string message = $"Empleado creado:\n" +
                             $"Nombre: {employee.Name} {employee.PaternalSurname} {employee.MaternalSurname}\n" +
                             $"Número de empleado: {employee.EmployeeNumber}\n" +
                             $"Dirección: {employee.Street} {employee.Number}, CP: {employee.CP}, Ciudad: {employee.City}\n" +
                             $"Teléfono: {employee.Phone}\n" +
                             $"Correo: {employee.Email}\n" +
                             $"CURP: {employee.CURP}\n" +
                             $"RFC: {employee.RFC}\n" +
                             $"Puesto: {employee.PositionVendor}\n" +
                             $"Sucursal: {employee.Branch}\n" +
                             $"Usuario: {employee.Username}\n" +
                             $"Contraseña: {employee.Password}";

            // Mostrar el empleado en un MessageBox
            MessageBox.Show(message, "Empleado Registrado", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
