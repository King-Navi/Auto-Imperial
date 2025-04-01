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

        public RelayCommand NavegateToHomeView { get; set; }
        public RelayCommand NavegateToClientsView { get; set; }
        public RelayCommand NavegateToVehiclesView { get; set; }
        public RelayCommand NavegateToSellView { get; set; }
        public RelayCommand NavegateToLoginView { get; set; }
        public SideBarViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
            NavegateToHomeView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<HomeViewModel>();
                },
                o => true);

            Navigation = navigationService;
            NavegateToClientsView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchClientViewModel>();
                },
                o => true);

            Navigation = navigationService;
            NavegateToVehiclesView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchVehicleViewModel>();
                },
                o => true);

            Navigation = navigationService;
            NavegateToSellView = new RelayCommand(
                o =>
                {
                    //Navigation.NavigateTo<SearchReservationViewModel>(); TODO
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
