using Services.Navigation;
using WpfClient.Utilities;
using WpfClient.MVVM.Model;
using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;

namespace WpfClient.MVVM.ViewModel
{
    internal class LogInViewModel : Services.Navigation.ViewModel
    {
        private readonly UserService user;
        public RelayCommand NavegateToHomeViewCommand { get; set; }
        private readonly IUserRepository _userRepository;

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
            set { username = value; OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        public LogInViewModel(INavigationService navigationService, UserService newUser, IUserRepository newUserRepository)
        {
            user = newUser;
            Navigation = navigationService;
            _userRepository = newUserRepository;

            NavegateToHomeViewCommand = new RelayCommand(
                o =>
                {
                    ErrorMessage = string.Empty;
                    
                    if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                    {
                        ErrorMessage = "Por favor, ingresa usuario y contraseña.";
                        return;
                    }

                    User authenticatedUser = _userRepository.Authenticate(Username, Password);

                    if (authenticatedUser == null)
                    {
                        ErrorMessage = "Credenciales inválidas. Intenta de nuevo.";
                        return;
                    }

                    user.SaveUser(authenticatedUser.Username, authenticatedUser.Password, authenticatedUser.Role, authenticatedUser.Id);

                    if (authenticatedUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        Mediator.Notify(MediatorKeys.SHOW_ADMIN_SIDE_BAR, null);
                    }
                    else
                    {
                        Mediator.Notify(MediatorKeys.SHOW_SIDE_BAR, null);
                    }

                    Navigation.NavigateTo<HomeViewModel>();
                },
                o => true);
        }

    }
}