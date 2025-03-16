using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class AdminSideBarViewModel : Services.Navigation.ViewModel
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
        public RelayCommand NavegateToEployesView { get; set; }
        public RelayCommand NavegateToSellsView { get; set; }
        public RelayCommand NavegateToSupliersView { get; set; }
        public RelayCommand NavegateToReportsView { get; set; }
        public RelayCommand NavegateToLoginView { get; set; }
        public AdminSideBarViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
            NavegateToHomeView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<HomeViewModel>();
                },
                o => true);

            NavegateToEployesView = new RelayCommand(
                o =>
                {
                    //Navigation.NavigateTo<SearchClientViewModel>(); TODO Change page
                },
                o => true);
            NavegateToSellsView = new RelayCommand(
                o =>
                {
                    //Navigation.NavigateTo<SearchClientViewModel>(); TODO Change page
                },
                o => true);

            NavegateToSupliersView = new RelayCommand(
                o =>
                {
                    //Navigation.NavigateTo<SearchClientViewModel>(); TODO Change page
                },
                o => true);

            NavegateToReportsView = new RelayCommand(
                o =>
                {
                    //Navigation.NavigateTo<SearchClientViewModel>(); TODO Change page
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
