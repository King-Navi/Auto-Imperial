using Services.Navegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;

namespace WpfCliente.MVVM.ViewModel
{
    internal class RegisterClientViewModel : Services.Navegation.ViewModel
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
        public RelayCommand NavigateToSearchClientView { get; set; }
        public RegisterClientViewModel(INavegationService navegationService)
        {
            Navegation = navegationService;
            NavigateToSearchClientView = new RelayCommand(
                o =>
                {
                    Navegation.NavigateTo<SearchClientViewModel>();
                },
                o => true);
        }
    }
}
