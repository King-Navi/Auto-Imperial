using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class SideBarViewModel : Services.Navigation.ViewModel
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
        public RelayCommand NavegateToLoginView { get; set; }
        public SideBarViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
            NavegateToRegisterClientView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<RegisterClientViewModel>();
                },
                o => true);

            NavegateToLoginView = new RelayCommand(
                o =>
                {
                    Mediator.Notify(MediatorKeys.HIDE_SIDE_BAR, null);
                    Navigation.NavigateTo<LogInViewModel>();
                },
                o => true);
        }
    }
}
