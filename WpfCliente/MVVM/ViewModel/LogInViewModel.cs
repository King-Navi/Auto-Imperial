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

        private readonly UserService _usuarioService;
        private string _nombreUsuario;

        public string NombreUsuario
        {
            get => _nombreUsuario;
            set { _nombreUsuario = value; OnPropertyChanged(); }
        }


        public RelayCommand NavegateToHomeViewCommand { get; set; }
        public LogInViewModel(INavegationService navegationService, UserService usuarioService)
        {
            _usuarioService = usuarioService;
            Navegation = navegationService;
            NavegateToHomeViewCommand = new RelayCommand(
                o =>
                {
                    _usuarioService.GuardarUsuario(NombreUsuario,"");
                    Mediator.Notify("ShowSideBar", null);
                    Navegation.NavigateTo<HomeViewModel>();
                },
                o => true);
        }
    }
}
