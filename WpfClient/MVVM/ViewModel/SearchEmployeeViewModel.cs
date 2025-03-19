using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Resources.ViewCards;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchEmployeeViewModel : Services.Navigation.ViewModel
    {
        private IEmployeeRepository _employeeRepository;
        public ObservableCollection<EmployeeCardViewModel> EmployeesList { get; set; } = new ObservableCollection<EmployeeCardViewModel>();


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

        public ICommand NavigateToRegisterEmployeeView { get; set; }

        public SearchEmployeeViewModel(INavigationService navigationService, UserService currentUser, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            Navigation = navigationService;

            SearchCommand = new RelayCommand(
                async o =>
                {
                    EmployeesList.Clear();
                    if (!String.IsNullOrWhiteSpace(SearchText))
                    {
                        var employees = await SearchEmployeesAsync();

                        foreach (var newEmployee in employees)
                        {
                            EmployeesList.Add(new EmployeeCardViewModel(navigationService, newEmployee));
                        }
                    }
                },
                o => !String.IsNullOrWhiteSpace(SearchText));


            Navigation = navigationService;
            NavigateToRegisterEmployeeView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<RegisterEmployeeViewModel>();
                },
                o => true);
        }

        private void NavigateToRegisterEmployee()
        {
            Navigation.NavigateTo<RegisterEmployeeViewModel>();
        }

        private async Task<List<Employee>> SearchEmployeesAsync()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(SearchText))
                {
                    var result = await _employeeRepository.SearchByCurpRfcNameAsync(
                        SearchText, AccountStatusEnum.Activo);
                    if (result.FirstOrDefault().idVendedor == -1)
                    {
                        ErrorMessage = "No se encontraron empleados con los datos proporcionados";
                    }
                    else 
                    {
                        ErrorMessage = string.Empty;
                        return ConvertToEmployeeList(result);
                    }     
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la búsqueda de empleados: {ex.Message}");
            }
            return new List<Employee>();
        }

        private List<Employee> ConvertToEmployeeList(List<Vendedor> list)
        {
            List<Employee> employees = new List<Employee>();

            foreach (var emp in list)
            {
                Employee newEmployee = new Employee();
                newEmployee.IdEmployee = emp.idVendedor;
                newEmployee.CURP = emp.CURP;
                newEmployee.CP = emp.codigoPostal;
                newEmployee.City = emp.ciudad;
                newEmployee.Password = emp.password;
                newEmployee.State = emp.estadoCuenta;
                newEmployee.Name = emp.nombre;
                newEmployee.PaternalSurname = emp.apellidoPaterno;
                newEmployee.MaternalSurname = emp.apellidoMaterno;
                newEmployee.Phone = emp.telefono;
                newEmployee.Email = emp.correo;
                newEmployee.Street = emp.calle;
                newEmployee.Number = emp.numero;
                newEmployee.RFC = emp.RFC;
                newEmployee.PositionVendor = emp.puestoVendedor;
                newEmployee.Username = emp.nombreUsuario;
                newEmployee.EmployeeNumber = emp.numeroEmpleado;
                newEmployee.Branch = emp.sucursal;
                newEmployee.Reservas = emp.Reservas;

                employees.Add(newEmployee);
            }
            return employees;
        }


    }
}
