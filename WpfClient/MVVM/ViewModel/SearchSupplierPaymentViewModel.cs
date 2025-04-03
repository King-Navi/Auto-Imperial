using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using System.IO;
using AutoImperialDAO.Enums;
using WpfClient.Resources.ViewCards;
using System.Windows.Navigation;

namespace WpfClient.MVVM.ViewModel
{
    class SearchSupplierPaymentViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private readonly ISupplierPaymentRepository _supplierPaymentRepository;
        private readonly INavigationService _navigation;

        private Supplier actualSupplier;
        public Supplier ActualSupplier
        {
            get => actualSupplier;
            set
            {
                actualSupplier = value;
                OnPropertyChanged();
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }
        public int SupplierId { get; set; }
        public ObservableCollection<SupplierPaymentCardViewModel> SupplierPaymentsList { get; } = new();

        public RelayCommand SearchCommand { get; }
        public RelayCommand NavigateToInfoSupplierView { get; }

        public SearchSupplierPaymentViewModel(INavigationService navigationService, ISupplierPaymentRepository supplierPaymentRepository)
        {
            _navigation = navigationService;
            _supplierPaymentRepository = supplierPaymentRepository;
            NavigateToInfoSupplierView = new RelayCommand(() => _navigation.NavigateTo<InfoSupplierViewModel>(ActualSupplier));
            SearchCommand = new RelayCommand(async () => await LoadPaymentsAsync());
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Supplier supplier)
            {
                ActualSupplier = supplier;
                SupplierId = supplier.SupplierId;
                _ = LoadPaymentsAsync();
            }
            else
            {
                MessageBox.Show("Error al cargar el proveedor");
            }
        }

        private async Task LoadPaymentsAsync()
        {
            SupplierPaymentsList.Clear();

            try
            {
                var payments = await _supplierPaymentRepository.GetPaymentsBySupplierIdAsync(SupplierId);
                if (payments.Count() == 0)
                {
                    ErrorMessage = "No se encontraron compras asociadas a este proveedor";
                }
                else
                {
                    ErrorMessage = string.Empty;
                }

                foreach (var pago in payments)
                {
                    SupplierPaymentsList.Add(new SupplierPaymentCardViewModel(_navigation, Convert(pago)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar pagos: {ex.Message}");
            }
        }

        private SupplierPayment Convert(CompraProveedor cp) => new()
        {
            SupplierPaymentId = cp.idCompraProveedor,
            SupplierId = cp.idProveedor,
            TotalAmount = cp.montoTotal,
            Folio = cp.folio,
            PurchaseDate = cp.fechaCompra,
            AdministratorId = cp.idAdministrador
        };
    }
}