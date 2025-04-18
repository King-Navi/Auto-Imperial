using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Windows.Input;
using System.Windows;
using WpfClient.Utilities;
using System.Text.RegularExpressions;
using WpfClient.Utilities.Enum;
using WpfClient.MVVM.Model;

namespace WpfClient.MVVM.ViewModel
{
    public class RegisterSellViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private string clientPhone;
        public string ClientPhone
        {
            get => clientPhone;
            set { clientPhone = value; OnPropertyChanged(); }
        }

        private string clientName;
        public string ClientName
        {
            get => clientName;
            set { clientName = value; OnPropertyChanged(); }
        }

        private string vehicle;
        public string Vehicle
        {
            get => vehicle;
            set { vehicle = value; OnPropertyChanged(); }
        }

        private string totalAmount;
        public string TotalAmount
        {
            get => totalAmount;
            set { totalAmount = value; OnPropertyChanged(); }
        }

        private DateTime? purchaseDate;
        public DateTime? PurchaseDate
        {
            get => purchaseDate;
            set { purchaseDate = value; OnPropertyChanged(); }
        }

        private string paymentMethod;
        public string PaymentMethod
        {
            get => paymentMethod;
            set { paymentMethod = value; OnPropertyChanged(); }
        }

        private string additionalNotes;
        public string AdditionalNotes
        {
            get => additionalNotes;
            set { additionalNotes = value; OnPropertyChanged(); }
        }

        private int reservationId;
        public int ReservationId
        {
            get => reservationId;
            set { reservationId = value; OnPropertyChanged(); }
        }

        private int vehicleId;
        public int VehicleId
        {
            get => vehicleId;
            set { vehicleId = value; OnPropertyChanged(); }
        }

        private string adminName;
        public string AdminName
        {
            get => adminName;
            set { adminName = value; OnPropertyChanged(); }
        }

        private INavigationService navigation;
        public INavigationService Navigation
        {
            get => navigation;
            set { navigation = value; OnPropertyChanged(); }
        }

        public ICommand NavigateToPreviousViewCommand { get; set; }
        public ICommand RegisterSellCommand { get; set; }

        private readonly ISellRepository _sellRepository;
        private readonly IDialogService _dialogService;

        public RegisterSellViewModel(INavigationService navigationService, ISellRepository sellRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _sellRepository = sellRepository;

            NavigateToPreviousViewCommand = new RelayCommand(NavigateToPreviousView);
            RegisterSellCommand = new RelayCommand(RegisterSell);
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is ReserveCardModel reserveCard)
            {
                ClientName = reserveCard.Client.Name + " " + reserveCard.Client.PaternalSurname;
                ClientPhone = reserveCard.Client.Phone;
                Vehicle = reserveCard.VehicleName;
                ReservationId = reserveCard.Reserve.idReserva;
                VehicleId = reserveCard.Reserve.idVersion;
            }
        }

        private void NavigateToPreviousView(object obj)
        {
            var confirmationVM = new ConfirmationViewModel(
                "Cancelar registro",
                "¿Está seguro que desea cancelar el registro? Se perderán los cambios no guardados.",
                ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Navigation.NavigateTo<SearchClientViewModel>();
            }
        }

        private void RegisterSell(object obj)
        {
            if (!ValidateFields())
                return;

            var confirmationVM = new ConfirmationViewModel(
                "Confirmación de registro",
                "¿Está seguro que desea registrar esta venta?",
                ConfirmationIconType.RegisterIcon);

            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                try
                {
                    if (!decimal.TryParse(TotalAmount, out decimal price))
                    {
                        _dialogService.ShowDialog(new AlertViewModel(
                            "Monto inválido",
                            "El monto total debe ser un valor numérico.",
                            AlertIconType.AlertIcon));
                        return;
                    }

                    if (PurchaseDate == null)
                    {
                        _dialogService.ShowDialog(new AlertViewModel(
                            "Fecha inválida",
                            "Debe seleccionar una fecha de compra.",
                            AlertIconType.AlertIcon));
                        return;
                    }

                    Venta sale = new Venta
                    {
                        fechaVenta = DateOnly.FromDateTime(PurchaseDate.Value),
                        precioVehiculo = price,
                        formaPago = PaymentMethod,
                        notasAdicionales = AdditionalNotes,
                        idReserva = ReservationId
                        // idVehiculo se asignará automáticamente según disponibilidad
                    };

                    var idVersion = VehicleId; 

                    if (_sellRepository.RegisterSaleWithStockCheck(sale, idVersion))
                    {
                        MessageBox.Show("Venta registrada correctamente.");
                        Navigation.NavigateTo<SearchClientViewModel>();
                    }
                    else
                    {
                        _dialogService.ShowDialog(new AlertViewModel(
                            "Sin stock disponible",
                            "No hay vehículos disponibles para la versión solicitada.",
                            AlertIconType.AlertIcon));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar la venta: " + ex.Message);
                }
            }
        }


        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(ClientName) ||
                string.IsNullOrWhiteSpace(TotalAmount) ||
                PurchaseDate == null ||
                string.IsNullOrWhiteSpace(PaymentMethod))
            {
                _dialogService.ShowDialog(new AlertViewModel(
                    "Campos vacíos",
                    "Todos los campos obligatorios deben ser completados.",
                    AlertIconType.AlertIcon));
                return false;
            }

            if (!decimal.TryParse(TotalAmount, out _))
            {
                _dialogService.ShowDialog(new AlertViewModel(
                    "Monto inválido",
                    "El monto total debe ser un valor numérico.",
                    AlertIconType.AlertIcon));
                return false;
            }

            return true;
        }
    }
}
