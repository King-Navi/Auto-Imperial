using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
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
            Navigation.NavigateTo<RegisterSellViewModel>();
        }

        private void CancelReservation(object obj)
        {
            // TODO: Cancelation
        }
    }
}
