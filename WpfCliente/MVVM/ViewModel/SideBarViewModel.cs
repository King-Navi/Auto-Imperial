using Services.Navegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;

namespace WpfCliente.MVVM.ViewModel
{
    class SideBarViewModel : Services.Navegation.ViewModel
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

        public RelayCommand NavegateToRegisterClientView { get; set; }
        public RelayCommand NavegateToLoginView { get; set; }
        public SideBarViewModel(INavegationService navegationService)
        {
            Navegation = navegationService;
            NavegateToRegisterClientView = new RelayCommand(
                o =>
                {
                    Navegation.NavigateTo<RegisterClientViewModel>();
                },
                o => true);

            NavegateToLoginView = new RelayCommand(
                o =>
                {
                    Mediator.Notify(MediatorKeys.HIDE_SIDE_BAR, null);
                    Navegation.NavigateTo<LogInViewModel>();
                },
                o => true);
        }
    }
}
