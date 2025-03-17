using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.MVVM.Model;
using WpfClient.Resources.ViewCards;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchEmployeeViewModel : Services.Navigation.ViewModel
    {

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

        public SearchEmployeeViewModel(INavigationService navigationService, UserService currentUser)
        {
            Navigation = navigationService;

            //TODO Omg so much hard code
            EmployeesList.Add(new EmployeeCardViewModel(navigationService, new Employee
            {
                Name = "Maria Jose"
            }));

            EmployeesList.Add(new EmployeeCardViewModel(navigationService, new Employee
            {
                Name = "Carlos Hernández"
            }));
            EmployeesList.Add(new EmployeeCardViewModel(navigationService, new Employee
            {
                Name = "Carlos Hernández"
            }));
            EmployeesList.Add(new EmployeeCardViewModel(navigationService, new Employee
            {
                Name = "Carlos Hernández"
            }));
            EmployeesList.Add(new EmployeeCardViewModel(navigationService, new Employee
            {
                Name = "Carlos Hernández"
            }));
        }


       
    }
}
