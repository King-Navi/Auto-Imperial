using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.ViewModel;
using WpfClient.Utilities;

namespace WpfClient.Resources.ViewCards
{
    class EmployeeCardViewModel : Services.Navigation.ViewModel
    {
        private Employee _employee = new Employee();

        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeePosition { get; set; }

        public Employee Employee
    {
        get => _employee;
        set
        {
            _employee = value;
            OnPropertyChanged();
        }
    }
    public ICommand NavigateToViewEmployeeViewCommand { get; set; }

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

    public EmployeeCardViewModel(INavigationService navigationService, Employee employee)
    {
        Employee = employee;
        EmployeeName = employee.Name + " " + employee.PaternalSurname + " " + employee.MaternalSurname;
        EmployeeNumber = "Número de empleado: " + employee.Number;
        EmployeePosition = "Puesto: " + employee.PositionVendor;
        Navigation = navigationService;
        NavigateToViewEmployeeViewCommand = new RelayCommand(
            o =>
            {
               Navigation.NavigateTo<InfoEmployeeViewModel>(Employee);  //TODO Change this
            },
            o => true);
    }

}
    
}
