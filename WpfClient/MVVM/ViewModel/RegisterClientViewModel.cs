using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    internal class RegisterClientViewModel : Services.Navigation.ViewModel
    {
        private 
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
        public RelayCommand NavigateToSearchClientView { get; set; }
        public RegisterClientViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
            NavigateToSearchClientView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchClientViewModel>();
                },
                o => true);
        }
    }
}
