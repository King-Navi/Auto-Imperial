using Services.Navigation;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.ViewModel;
using WpfClient.Utilities;

namespace WpfClient.Resources.ViewCards
{
    public class ReserveCardViewModel :  Services.Navigation.ViewModel
    {
        public ReserveCardModel Model { get; }
        private INavigationService navigation;
        public INavigationService Navigation
        {
            get => navigation;
            set { navigation = value; OnPropertyChanged(); }
        }
        public bool ShowButtons => Model.ReservationStatus == AutoImperialDAO.Enums.ReserveStatusEnum.Interesado;

        public ICommand BuyVehicleCommand { get; }
        public ICommand CancelReservationCommand { get; }
        
        public ReserveCardViewModel(INavigationService navigationService, ReserveCardModel model)
        {
            Navigation = navigationService;
            Model = model;

            BuyVehicleCommand = new RelayCommand(
                execute: BuyVehicle,
                canExecute: _ => ShowButtons
            );

            CancelReservationCommand = new RelayCommand(
                execute: CancelReservation,
                canExecute: _ => ShowButtons
            );
        }

        private void BuyVehicle(object obj)
        {
            Navigation.NavigateTo<RegisterSellViewModel>(Model);
        }

        private void CancelReservation(object obj)
        {
            // TODO: Cancelation
        }
    }
}
