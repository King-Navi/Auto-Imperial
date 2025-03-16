using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
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
        private ObservableCollection<ClientCardViewModel> clientsList;
        public ObservableCollection<ClientCardViewModel> ClientsList { get => clientsList; set => clientsList = value; }

        public RelayCommand NavegateToRegisterClientView { get; set; }

        public SearchClientViewModel(INavigationService navigationService)
        {
            //TEST
            ClientsList = new ObservableCollection<ClientCardViewModel>
            {
                new ClientCardViewModel(navigationService , new Client {Name = "aGREGADO"}),
             };
            //TEST

            Navigation = navigationService;
            NavegateToRegisterClientView = new RelayCommand(
                o =>
                {
                    if (true)
                    {
                        Client selected  = new Client {Name = "Juan" };
                        Navigation.NavigateTo<RegisterClientViewModel>(selected);
                    }
                    else
                    {
                        Navigation.NavigateTo<RegisterClientViewModel>();
                    }
                },
                o => true);
        }
    }
}
