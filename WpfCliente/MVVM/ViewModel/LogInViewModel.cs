using Services.Navegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;
using WpfCliente.MVVM.Model;

namespace WpfCliente.MVVM.ViewModel
{
    internal class LogInViewModel : Services.Navegation.ViewModel
    {

        private readonly UserService user;
        public RelayCommand NavegateToHomeViewCommand { get; set; }

        private INavegationService navegation;
        public INavegationService Navegation
        {
            get => navegation;
            set
            {
                navegation = value;
                OnPropertyChanged();
            }
        }

        
        private string username;

        public string Username
        {
            get => username;
            set { username= value; OnPropertyChanged(); }
        }

       
        public LogInViewModel(INavegationService navegationService, UserService newUser)
        {
            user = newUser;
            Navegation = navegationService;
            NavegateToHomeViewCommand = new RelayCommand(
                o =>
                {
                    user.SaveUser(Username,""); //TODO this is a hardcode for login user
                    Mediator.Notify(MediatorKeys.SHOW_SIDE_BAR, null);
                    Navegation.NavigateTo<HomeViewModel>();
                },
                o => true);
        }
    }
}
