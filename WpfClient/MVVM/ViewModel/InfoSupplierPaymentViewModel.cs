using AutoImperialDAO.DAO.Interfaces;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public ICommand NavigateToSearchSupplierPaymentCommand { get; set; }
        public ICommand RegisterVehiclesCommand { get; set; }
        public InfoSupplierPaymentViewModel(INavigationService navigationService, IDialogService dialogService, ISupplierRepository supplierRepository)
        {
            _dialogService = dialogService;
            _supplierRepository = supplierRepository;
            Navigation = navigationService;

            NavigateToSearchSupplierPaymentCommand = new RelayCommand(NavigateToSearchSupplierPayment);
            RegisterVehiclesCommand = new RelayCommand(RegisterVehicle);
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is SupplierPayment supplierPayment)
            {
                ActualSupplierPayment = supplierPayment;
                _originalSupplierPayment = (SupplierPayment)supplierPayment.Clone();

                _ = InitPropertiesAsync(supplierPayment.SupplierId);
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

            MessageBox.Show("" + supplierPaymentId);

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
