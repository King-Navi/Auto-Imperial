using Services.Navigation;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.ViewModel;
using WpfClient.Utilities;

namespace WpfClient.Resources.ViewCards
{
    class SellCardViewModel : Services.Navigation.ViewModel
    {
        private Sell _sell = new Sell();

        public string Vehicle { get; set; }
        public string VehiclePrice => "$" + Sell.VehiclePrice;
        public string DateText => Sell.SellDate.ToString("dd/MM/yyyy");
        public string Client { get; set; }

        public Sell Sell
        {
            get => _sell;
            set
            {
                _sell = value;
                OnPropertyChanged();
            }
        }
        public ICommand NavigateToViewSellViewCommand { get; set; }

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

        public SellCardViewModel(INavigationService navigationService, Sell sell)
        {
            Sell = sell;
            Vehicle = 
                sell.Vehicle.idVersionNavigation.idModeloNavigation.idMarcaNavigation.nombre + ", " +
                sell.Vehicle.idVersionNavigation.idModeloNavigation.nombre + ", " +
                sell.Vehicle.idVersionNavigation.nombre;
            
            Client = 
                sell.idReservaNavigation.idClienteNavigation.nombre + " " +
                sell.idReservaNavigation.idClienteNavigation.apellidoPaterno;
            Navigation = navigationService;
            NavigateToViewSellViewCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<InfoSellViewModel>(Sell);
                },
                o => true);
        }

    }

}
