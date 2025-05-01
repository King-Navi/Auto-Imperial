using Services.Navigation;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.ViewModel;
using WpfClient.Utilities;

namespace WpfClient.Resources.ViewCards
{
    class EmployeeCardViewModel : Services.Navigation.ViewModel
    {
        private SellerEmployee _employee = new SellerEmployee();

        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeePosition { get; set; }

        public SellerEmployee Employee
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

    public EmployeeCardViewModel(INavigationService navigationService, SellerEmployee employee)
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
