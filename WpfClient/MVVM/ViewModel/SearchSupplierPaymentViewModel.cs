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

        public int SupplierId { get; set; }
        public ObservableCollection<SupplierPaymentCardViewModel> SupplierPaymentsList { get; } = new();

        public RelayCommand SearchCommand { get; }

        public SearchSupplierPaymentViewModel(INavigationService navigationService, ISupplierPaymentRepository supplierPaymentRepository)
        {
            _navigation = navigationService;
            _supplierPaymentRepository = supplierPaymentRepository;

            SearchCommand = new RelayCommand(async () => await LoadPaymentsAsync());
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Supplier supplier)
            {
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
            SupplierId = cp.idProveedor,
            TotalAmount = cp.montoTotal,
            Folio = cp.folio,
            PurchaseDate = cp.fechaCompra,
            AdministratorId = cp.idAdministrador
        };
    }
}