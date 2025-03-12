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
    internal class HomeViewModel : Services.Navegation.ViewModel
    {
        public RelayCommand NavegateToLogInViewCommand { get; set; }
        private readonly UserService user;


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

        public string Username => user.CurrentUser?.Name ?? "Invitado";

        public HomeViewModel(INavegationService navegationService , UserService currentUser)
        {
            user = currentUser;
            Navegation = navegationService;
            Navegation.NavigateTo<SearchClientViewModel>();
            NavegateToLogInViewCommand = new RelayCommand(
                o =>
                {
                    Navegation.NavigateTo<LogInViewModel>();

                },
                o => true);
        }
    }
}
