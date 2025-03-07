using Services.Navegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;

namespace WpfCliente.MVVM.ViewModel
{
    internal class SearchClientViewModel : Services.Navegation.ViewModel
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
        public SearchClientViewModel(INavegationService navegationService)
        {
            Navegation = navegationService;
            NavegateToRegisterClientView = new RelayCommand(
                o =>
                {
                    Navegation.NavigateTo<RegisterClientViewModel>();
                },
                o => true);
        }
    }
}
