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
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Resources.ViewCards;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchSupplierViewModel : Services.Navigation.ViewModel
    {
        private ISupplierRepository _supplierRepository;
        public ObservableCollection<SupplierCardViewModel> SuppliersList { get; set; } = new ObservableCollection<SupplierCardViewModel>();


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

        public ICommand NavigateToRegisterSupplierView { get; set; }

        public SearchSupplierViewModel(INavigationService navigationService, UserService currentUser, IEmployeeRepository employeeRepository)
        {
            //_supplierRepository = supplierRepository;
            //Navigation = navigationService;

            //SearchCommand = new RelayCommand(
            //    async o =>
            //    {
            //        SuppliersList.Clear();
            //        if (!String.IsNullOrWhiteSpace(SearchText))
            //        {
            //            var suppliers = await SearchSuppliersAsync();

            //            foreach (var newSupplier in suppliers)
            //            {
            //                SuppliersList.Add(new EmployeeCardViewModel(navigationService, newEmployee));
            //            }
            //        }
            //    },
            //    o => !String.IsNullOrWhiteSpace(SearchText));


            Navigation = navigationService;
            NavigateToRegisterSupplierView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<RegisterSupplierViewModel>();
                },
                o => true);
        }

        private void NavigateToRegisterSupplier()
        {
            Navigation.NavigateTo<RegisterSupplierViewModel>();
        }

        //private async Task<List<Supplier>> SearchEmployeesAsync()
        //{
        //    try
        //    {
        //        if (!String.IsNullOrWhiteSpace(SearchText))
        //        {
        //            var result = await _supplierRepository.SearchByCurpRfcNameAsync(
        //                SearchText, AccountStatusEnum.Activo);
        //            if (result.FirstOrDefault().idVendedor == -1)
        //            {
        //                ErrorMessage = "No se encontraron empleados con los datos proporcionados";
        //            }
        //            else
        //            {
        //                ErrorMessage = string.Empty;
        //                return ConvertToEmployeeList(result);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error en la búsqueda de empleados: {ex.Message}");
        //    }
        //    return new List<Employee>();
        //}

        //private List<Employee> ConvertToEmployeeList(List<Vendedor> list)
        //{
        //    List<Employee> employees = new List<Employee>();

        //    foreach (var emp in list)
        //    {
        //        Employee newEmployee = new Employee();
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
        //        newEmployee.Reservas = emp.Reservas;

        //        employees.Add(newEmployee);
        //    }
        //    return employees;
        //}
    }
}
