﻿using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;
using WpfCliente.MVVM.Model;

namespace WpfCliente.MVVM.ViewModel
{
    internal class HomeViewModel : Services.Navigation.ViewModel
    {
        public RelayCommand NavegateToLogInViewCommand { get; set; }
        private readonly UserService user;


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

        public string Username => user.CurrentUser?.Name ?? "Invitado";

        public HomeViewModel(INavigationService navigationService , UserService currentUser)
        {
            user = currentUser;
            Navigation = navigationService;
            Navigation.NavigateTo<SearchClientViewModel>();
            NavegateToLogInViewCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<LogInViewModel>();

                },
                o => true);
        }
    }
}
