﻿using Services.Navigation;
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
        public RelayCommand NavegateToEmployeesView { get; set; }
        public RelayCommand NavegateToSellsView { get; set; }
        public RelayCommand NavegateToSuppliersView { get; set; }
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

            NavegateToEmployeesView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchEmployeeViewModel>(); 
                },
                o => true);
            NavegateToSellsView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchSellViewModel>();
                },
                o => true);

            NavegateToSuppliersView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchSupplierViewModel>();
                },
                o => true);

            NavegateToReportsView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<ReportsViewModel>();
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
