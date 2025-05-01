using AutoImperialDAO.DAO.Interfaces;
using Services.Dialogs;
using Services.Navigation;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using WpfClient.Utilities.Enum;

namespace WpfClient.MVVM.ViewModel
{
    public class EditSellViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private string clientName;
        public string ClientName
        {
            get => clientName;
            set { clientName = value; OnPropertyChanged(); }
        }

        private string totalAmount;
        public string TotalAmount
        {
            get => totalAmount;
            set { totalAmount = value; OnPropertyChanged(); }
        }

        private string? purchaseDate;
        public string? PurchaseDate
        {
            get => purchaseDate;
            set { purchaseDate = value; OnPropertyChanged(); }
        }

        private string vehicle;
        public string Vehicle
        {
            get => vehicle;
            set { vehicle = value; OnPropertyChanged(); }
        }

        private string aditionalNotes;
        public string AditionalNotes
        {
            get => aditionalNotes;
            set { aditionalNotes = value; OnPropertyChanged(); }
        }

        private string clientPhone;
        public string ClientPhone
        {
            get => clientPhone;
            set { clientPhone = value; OnPropertyChanged(); }
        }


        private string paymentMethod;
        public string PaymentMethod
        {
            get => paymentMethod;
            set { paymentMethod = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> PaymentMethods { get; set; } 

        public ICommand NavigateToSearchSellView { get; set; }
        public ICommand RegisterSupplierPaymentCommand { get; set; }

        private Sell _actualSell = new Sell();
        public Sell ActualSell
        {
            get => _actualSell;
            set { _actualSell = value; OnPropertyChanged(); }
        }

        private readonly INavigationService navigation;
        public INavigationService Navigation
        {
            get => navigation;
        }

        private readonly ISellRepository _sellRepository;
        private readonly IDialogService _dialogService;

        public EditSellViewModel(INavigationService navigationService, ISellRepository sellRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _sellRepository = sellRepository;
            navigation = navigationService;

            InicializatePaymentMethods();

            NavigateToSearchSellView = new RelayCommand(NavigateToSearchSell);
            RegisterSupplierPaymentCommand = new RelayCommand(EditSell);
        }

        private void InicializatePaymentMethods()
        {
            PaymentMethods = new ObservableCollection<string>
            {
                "Contado",
                "Pago a plazos",
                "Meses sin intereses"
            };
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Sell sell)
            {
                ActualSell = sell;
                InitProperties();
            }
            else
            {
                MessageBox.Show("Error al cargar la venta");
            }
        }

        private void InitProperties()
        {
            ClientName = 
                ActualSell.idReservaNavigation.idClienteNavigation.nombre + " " +
                ActualSell.idReservaNavigation.idClienteNavigation.apellidoPaterno + " " +
                ActualSell.idReservaNavigation.idClienteNavigation.apellidoMaterno;
            TotalAmount = ActualSell.precioVehiculo.ToString();
            DateTime parsedDate;
            PurchaseDate = ActualSell.SellDate.ToString("dd/MM/yyyy");
            Vehicle = 
                ActualSell.idVehiculoNavigation.idVersionNavigation.idModeloNavigation.idMarcaNavigation.nombre + " " +
                ActualSell.idVehiculoNavigation.idVersionNavigation.idModeloNavigation.nombre + " " +
                ActualSell.idVehiculoNavigation.idVersionNavigation.nombre;
            
            AditionalNotes = ActualSell.AdditionalNotes;
            ClientPhone = ActualSell.idReservaNavigation.idClienteNavigation.telefono;
            PaymentMethod = ActualSell.PaymentMethod;
        }

        
        private void NavigateToSearchSell()
        {
            var confirmationVM = new ConfirmationViewModel("Cancelar edición",
                "¿Está seguro que desea cancelar la edición? Se perderán todos los cambios no guardados",
                ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Navigation.NavigateTo<SearchSellViewModel>();
            }
        }

        private void EditSell()
        {
            if (!ValidateFields())
                return;

            var confirmationVM = new ConfirmationViewModel("Confirmación de edición",
                "¿Está seguro que desea editar esta venta?",
                ConfirmationIconType.RegisterIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Sell updatedSell = new Sell
                {
                    idVenta = ActualSell.idVenta,

                    idReservaNavigation = ActualSell.idReservaNavigation,
                    idVehiculoNavigation = ActualSell.idVehiculoNavigation,
                    SellDate = ActualSell.SellDate,

                    precioVehiculo = decimal.TryParse(TotalAmount, out decimal amount) ? amount : ActualSell.precioVehiculo,
                    AdditionalNotes = AditionalNotes,
                    PaymentMethod = PaymentMethod
                };

                try
                {
                    if (_sellRepository.Edit(updatedSell))
                    {
                        MessageBox.Show("Venta editada correctamente");
                        Navigation.NavigateTo<SearchSellViewModel>();
                    }
                    else
                    {
                        MessageBox.Show("Error al editar la venta");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al editar la venta: " + e.Message);
                }
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(ClientName) ||
                string.IsNullOrWhiteSpace(TotalAmount) ||
                string.IsNullOrWhiteSpace(Vehicle) ||
                string.IsNullOrWhiteSpace(ClientPhone) ||
                string.IsNullOrWhiteSpace(PaymentMethod))
            {
                _dialogService.ShowDialog(new AlertViewModel("Campos vacíos",
                    "Se han dejado campos vacíos, todos los campos son obligatorios.",
                    AlertIconType.AlertIcon));
                return false;
            }

            if (!decimal.TryParse(TotalAmount, out _))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos",
                    "El monto total debe ser un número válido.",
                    AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(ClientPhone, @"^\d{10}$"))
            {
                _dialogService.ShowDialog(new AlertViewModel("Datos inválidos",
                    "El número de teléfono debe tener 10 dígitos.",
                    AlertIconType.AlertIcon));
                return false;
            }

            return true;
        }
    }
}
