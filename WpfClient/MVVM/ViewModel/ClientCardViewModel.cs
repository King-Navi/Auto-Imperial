using Services.Navigation;
using System.Windows.Input;
using WpfClient.MVVM.Model;
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
