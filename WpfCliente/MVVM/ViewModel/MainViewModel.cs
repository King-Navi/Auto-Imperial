using Services.Navegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;

namespace WpfCliente.MVVM.ViewModel
{
    public class MainViewModel : Services.Navegation.ViewModel
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
        public MainViewModel(INavegationService navegationService)
        {
            Navegation = navegationService;
            Navegation.NavigateTo<LogInViewModel>();
        }
    }
}
