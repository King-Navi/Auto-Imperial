using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SearchEmployeeViewModel : Services.Navigation.ViewModel
    {


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
            
        }
    }
}
