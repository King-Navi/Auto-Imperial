using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;
using WpfCliente.MVVM.Model;

namespace WpfCliente.MVVM.ViewModel
{
    internal class LogInViewModel : Services.Navigation.ViewModel
    {

        private readonly UserService user;
        public RelayCommand NavegateToHomeViewCommand { get; set; }

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

        
        private string username;

        public string Username
        {
            get => username;
            set { username= value; OnPropertyChanged(); }
        }

       
        public LogInViewModel(INavigationService navigationService, UserService newUser)
        {
            user = newUser;
            Navigation = navigationService;
            NavegateToHomeViewCommand = new RelayCommand(
                o =>
                {
                    user.SaveUser(Username,""); //TODO this is a hardcode for login user
                    Mediator.Notify(MediatorKeys.SHOW_SIDE_BAR, null);
                    Navigation.NavigateTo<HomeViewModel>();
                },
                o => true);
        }
    }
}
