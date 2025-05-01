using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class RegisterSupplierPaymentViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        public Supplier ActualSupplier { get; set; } = new Supplier();
        private readonly UserService user;
        public string AdminName => user.CurrentUser?.Name ?? "Invitado";

        private string? supplierName;
        public string? SupplierName
        {
            get => supplierName;
            set
            {
                supplierName = value;
                OnPropertyChanged();
            }
        }

        private string? totalAmount;
        public string? TotalAmount
        {
            get => totalAmount;
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
        }

        private DateTime? purchaseDate;
        public DateTime? PurchaseDate
        {
            get => purchaseDate;
            set
            {
                purchaseDate = value;
                OnPropertyChanged();
            }
        }

        private string? vehiclesNumber;
        public string? VehiclesNumber
        {
            get => vehiclesNumber;
            set
            {
                vehiclesNumber = value;
                OnPropertyChanged();
            }
        }



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

        public ICommand NavigateToInfoSupplierView { get; set; }
        public ICommand RegisterSupplierPaymentCommand { get; set; }

        private readonly ISupplierPaymentRepository _supplierPaymentRepository;
        private readonly IDialogService _dialogService;

        public RegisterSupplierPaymentViewModel(INavigationService navigationService, UserService currentUser, ISupplierPaymentRepository supplierPaymentRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _supplierPaymentRepository = supplierPaymentRepository;
            NavigateToInfoSupplierView = new RelayCommand(NavigateToInfoSupplier);
            RegisterSupplierPaymentCommand = new RelayCommand(RegisterSupplierPayment);
            user = currentUser;
        }

        private void NavigateToInfoSupplier()
        {
            var confirmationVM = new ConfirmationViewModel("Cancerlar registro", $"¿Esta seguro que desea salir del registro? Se perderán todos los cambios no guardados", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Navigation.NavigateTo<InfoSupplierViewModel>(ActualSupplier);
            }
        }
        private async void RegisterSupplierPayment()
        {
            var confirmationVM = new ConfirmationViewModel(
                "Confirmación de registro",
                "¿Está seguro que desea registrar esta nueva compra?",
                Utilities.Enum.ConfirmationIconType.RegisterIcon);

            var result = _dialogService.ShowDialog(confirmationVM);

            if (result == true)
            {
                try
                {
                    if (!decimal.TryParse(TotalAmount, out var monto))
                    {
                        MessageBox.Show("El monto total ingresado no es válido.");
                        return;
                    }

                    if (!int.TryParse(VehiclesNumber, out var numeroVehiculos))
                    {
                        MessageBox.Show("El número total de vehículos ingresado no es válido.");
                        return;
                    }

                    if (PurchaseDate == null)
                    {
                        MessageBox.Show("Debe seleccionar una fecha de compra.");
                        return;
                    }

                    var compra = new CompraProveedor
                    {
                        idProveedor = ActualSupplier.SupplierId,
                        idAdministrador = user.CurrentUser.Id,
                        montoTotal = monto,
                        folio = $"COMP-{DateTime.Now:yyyyMMddHHmmss}",
                        fechaCompra = DateOnly.FromDateTime(PurchaseDate.Value),
                        vehiculosComprados = numeroVehiculos
                    };

                    bool success = await _supplierPaymentRepository.RegisterSupplierPaymentAsync(compra);

                    if (success)
                    {
                        MessageBox.Show("Compra registrada correctamente.");
                        Navigation.NavigateTo<InfoSupplierViewModel>(ActualSupplier);
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar la compra.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
        }


        public void ReceiveParameter(object parameter)
        {
            if (parameter is Supplier supplier)
            {
                ActualSupplier = supplier;
                SupplierName = supplier.SupplierName;
            }
            else
            {
                MessageBox.Show("Error al cargar el proveedor");
            }
        }
    }
}
