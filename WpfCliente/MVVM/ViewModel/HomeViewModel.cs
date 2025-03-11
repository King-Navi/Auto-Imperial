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

        private INavegationService navegation2;
        public INavegationService InternalNavigationService
        {
            get => navegation2;
            set
            {
                navegation2 = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavegateToLogInViewCommand { get; set; }
        private readonly UserService _usuarioService;
        public string NombreUsuario => _usuarioService.UsuarioAutenticado?.Nombre ?? "Invitado";

        public HomeViewModel(INavegationService navegationService , INavegationServiceFactory f, UserService usuarioService)
        {
            _usuarioService = usuarioService;
            Navegation = navegationService;
            InternalNavigationService = f.CreateNavigationService();
            InternalNavigationService.NavigateTo<SearchClientViewModel>();
            NavegateToLogInViewCommand = new RelayCommand(
                o =>
                {
                    Navegation.NavigateTo<LogInViewModel>();

                },
                o => true);
        }
    }
}
