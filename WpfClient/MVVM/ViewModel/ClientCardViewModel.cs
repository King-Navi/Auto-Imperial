using Services.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.View;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    public class ClientCardViewModel : Services.Navigation.ViewModel
    {
        private Client _clientActual = new Client();

        public Client ClientActual
        {
            get => _clientActual;
            set
            {
                _clientActual = value;
                OnPropertyChanged();
            }
        }
        public ICommand NavigateToReserveViewCommand { get; set; }

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

        public ClientCardViewModel(INavigationService navigationService, Client client)
        {
            ClientActual = client;
                Navigation = navigationService;
            
            NavigateToReserveViewCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<ReserveViewModel>(_clientActual);
                },
                o => true);
        }

    }
}
