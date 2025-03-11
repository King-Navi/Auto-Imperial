using Services.Navegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;

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
        public RelayCommand NavegateToHomeViewCommand { get; set; }
        public LogInViewModel(INavegationService navegationService)
        {
            Navegation = navegationService;
            NavegateToHomeViewCommand = new RelayCommand(
                o =>
                {
                    Mediator.Notify("ShowSideBar", null);
                    Navegation.NavigateTo<HomeViewModel>();
                },
                o => true);
        }
    }
}
