using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCliente.Utilities;

namespace WpfCliente.MVVM.ViewModel
{
    internal class SearchClientViewModel : Services.Navigation.ViewModel
    {
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
        public RelayCommand NavegateToRegisterClientView { get; set; }
        public SearchClientViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
            NavegateToRegisterClientView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<RegisterClientViewModel>();
                },
                o => true);
        }
    }
}
