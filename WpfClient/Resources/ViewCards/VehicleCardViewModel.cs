using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using WpfClient.MVVM.ViewModel;

namespace WpfClient.Resources.ViewCards
{
    class VehicleCardViewModel : Services.Navigation.ViewModel
    {
        private Vehicle _vehicle = new Vehicle();

        public string VehicleName { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleYear { get; set; }

        public Vehicle Vehicle
        {
            get => _vehicle;
            set
            {
                _vehicle = value;
                OnPropertyChanged();
            }
        }
        public ICommand NavigateToViewVehicleViewCommand { get; set; }

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

        public VehicleCardViewModel(INavigationService navigationService, Vehicle vehicle)
        {
            Vehicle = vehicle;
            VehicleName = vehicle.Branch + " " + vehicle.Model + " " + vehicle.Version;
            VehicleColor = "Color: " + vehicle.Color;
            VehicleYear = "Año: " + vehicle.Year;
            Navigation = navigationService;
            NavigateToViewVehicleViewCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<InfoVehicleViewModel>(Vehicle);  //TODO Change this
                },
                o => true);
        }

    }
}
