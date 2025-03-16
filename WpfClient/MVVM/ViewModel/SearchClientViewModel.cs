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
    internal class SearchClientViewModel : Services.Navigation.ViewModel
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
        public RelayCommand NavegateToRegisterClientView { get; set; }
        public SearchClientViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
            NavegateToRegisterClientView = new RelayCommand(
                o =>
                {
                    if (true)
                    {
                        Client selected  = new Client {Name = "Juan" };
                        Navigation.NavigateTo<RegisterClientViewModel>(selected);
                    }
                    else
                    {
                        Navigation.NavigateTo<RegisterClientViewModel>();
                    }
                },
                o => true);
        }
    }
}
