using AutoImperialDAO.DAO.Interfaces;
using Services.Dialogs;
using Services.Navigation;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using AutoImperialDAO.Enums;
using Microsoft.Extensions.DependencyInjection;
using WpfClient.MVVM.View;

namespace WpfClient.MVVM.ViewModel
{
    class InfoSupplierPaymentViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private SupplierPayment _actualSupplierPayment = new SupplierPayment();
        private SupplierPayment? _originalSupplierPayment;
        private int NumberOfRegisterVehicules = int.MaxValue;

        private Supplier _actualSupplier = new Supplier();


        private string? folio;
        public string? Folio
        {
            get => folio;
            set
            {
                folio = value;
                OnPropertyChanged();
            }
        }

        private decimal? totalAmount;
        public decimal? TotalAmount
        {
            get => totalAmount;
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
        }

        private string? date;
        public string? Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

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

        private string? supplierCity;
        public string? SupplierCity
        {
            get => supplierCity;
            set
            {
                supplierCity = value;
                OnPropertyChanged();
            }
        }

        private string? supplierPhone;
        public string? SupplierPhone
        {
            get => supplierPhone;
            set
            {
                supplierPhone = value;
                OnPropertyChanged();
            }
        }

        private string? supplierEmail;
        public string? SupplierEmail
        {
            get => supplierEmail;
            set
            {
                supplierEmail = value;
                OnPropertyChanged();
            }
        }

        private string? numberRegisterVehicles;
        public string? NumberRegisterVehicles
        {
            get => numberRegisterVehicles;
            set
            {
                numberRegisterVehicles = value;
                OnPropertyChanged();
            }
        }



        public SupplierPayment ActualSupplierPayment
        {
            get => _actualSupplierPayment;
            set
            {
                _actualSupplierPayment = value;
                OnPropertyChanged();
            }
        }

        public Supplier ActualSupplier
        {
            get => _actualSupplier;
            set
            {
                _actualSupplier = value;
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
        private readonly IDialogService _dialogService;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierPaymentRepository _supplierPaymentRepository;

        public ICommand NavigateToSearchSupplierPaymentCommand { get; set; }
        public IRelayCommand RegisterVehiclesCommand { get; set; }
        public InfoSupplierPaymentViewModel(INavigationService navigationService, IDialogService dialogService, ISupplierRepository supplierRepository, ISupplierPaymentRepository supplierPaymentRepository)
        {
            _dialogService = dialogService;
            _supplierRepository = supplierRepository;
            _supplierPaymentRepository = supplierPaymentRepository;
            Navigation = navigationService;

            NavigateToSearchSupplierPaymentCommand = new RelayCommand(NavigateToSearchSupplierPayment);
            RegisterVehiclesCommand = new RelayCommand(
                execute: o => RegisterVehicle(),
                canExecute: o => NumberOfRegisterVehicules < ActualSupplierPayment.VehiclesCount
            );

            Mediator.Register(MediatorKeys.UPDATE_VEHICLES_REGISTER, args => UpdateVehiclesRegisters());
            
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is SupplierPayment supplierPayment)
            {
                ActualSupplierPayment = supplierPayment;
                _originalSupplierPayment = (SupplierPayment)supplierPayment.Clone();

                _ = InitPropertiesAsync(supplierPayment.SupplierId);
                UpdateVehiclesRegisters();
            }
            else
            {
                MessageBox.Show("Error al cargar el proveedor");
            }
        }

        private void NavigateToSearchSupplierPayment()
        {
            Navigation.NavigateTo<SearchSupplierPaymentViewModel>(ActualSupplier);
        }

        private void RegisterVehicle()
        {
            int supplierPaymentId = ActualSupplierPayment.SupplierPaymentId;

            RegisterVehicleViewModel viewModel = new RegisterVehicleViewModel(
                App.ServiceProvider.GetRequiredService<INavigationService>(),
                App.ServiceProvider.GetRequiredService<UserService>(),
                App.ServiceProvider.GetRequiredService<IDialogService>(),
                App.ServiceProvider.GetRequiredService<IVehicleRepository>(),
                supplierPaymentId
            );

            var window = new RegisterVehicleView(viewModel);
            window.ShowDialog();
        }

        private void UpdateVehiclesRegisters()
        {
            NumberOfRegisterVehicules = int.MaxValue;
            RegisterVehiclesCommand.RaiseCanExecuteChanged();

            NumberRegisterVehicles = "Cargando...";
            App.Current.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Delay(1000);
                try
                {
                    NumberOfRegisterVehicules = _supplierPaymentRepository.GetCountVehiclesById(ActualSupplierPayment.SupplierPaymentId);

                    NumberRegisterVehicles = $"{NumberOfRegisterVehicules} vehículos de {ActualSupplierPayment.VehiclesCount}";
                    RegisterVehiclesCommand.RaiseCanExecuteChanged();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los vehículos registrados" + ex);
                    NumberRegisterVehicles = $"Error en la carga";
                }
            });
                
        }

        private async Task InitPropertiesAsync(int supplierId)
        {
            var proveedor = await _supplierRepository.SearchByIdAsync(supplierId, AccountStatusEnum.Activo);

            if (proveedor == null)
            {
                MessageBox.Show("Proveedor no encontrado");
                return;
            }

            Supplier supplier = new Supplier
            {
                SupplierId = proveedor.idProveedor,
                SupplierName = proveedor.nombreProveedor,
                Street = proveedor.calle,
                Number = proveedor.numero,
                ZipCode = proveedor.codigoPostal,
                City = proveedor.ciudad,
                Phone = proveedor.telefono,
                Email = proveedor.correo,
                PrimaryContact = proveedor.contactoPrincipal
            };

            ActualSupplier = supplier;

            SupplierName = supplier.SupplierName;
            SupplierCity = supplier.City;
            SupplierPhone = supplier.Phone;
            SupplierEmail = supplier.Email;

            Folio = ActualSupplierPayment.Folio;
            TotalAmount = ActualSupplierPayment.TotalAmount;
            Date = ActualSupplierPayment.PurchaseDate.ToString("dd/MM/yyyy");
        }

    }
}
