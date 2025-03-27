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

namespace WpfClient.MVVM.ViewModel
{
    class SearchSupplierPaymentViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private int supplierId;
        public int SupplierId
        {
            get => supplierId;
            set { supplierId = value; OnPropertyChanged(); }
        }
        private string supplierName;
        public string SupplierName
        {
            get => supplierName;
            set { supplierName = value; OnPropertyChanged(); }
        }
        private string primaryContact;
        public string PrimaryContact
        {
            get => primaryContact;
            set { primaryContact = value; OnPropertyChanged(); }
        }
        private string phone;
        public string Phone
        {
            get => phone;
            set { phone = value; OnPropertyChanged(); }
        }
        private string city;
        public string City
        {
            get => city;
            set { city = value; OnPropertyChanged(); }
        }
        
        private ISupplierPaymentRepository _supplierPaymentRepository;

        private Supplier _actualSupplier = new Supplier();
        private Supplier? _originalSupplier;
        public Supplier ActualSupplier
        {
            get => _actualSupplier;
            set
            {
                _actualSupplier = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<SupplierPaymentCardViewModel> SupplierPaymentsList { get; set; } = new ObservableCollection<SupplierPaymentCardViewModel>();

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

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public IRelayCommand SearchCommand { get; set; }

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

        public SearchSupplierPaymentViewModel(INavigationService navigationService, UserService currentUser, ISupplierPaymentRepository supplierPaymentRepository)
        {
            _supplierPaymentRepository = supplierPaymentRepository;
            Navigation = navigationService;

            SearchCommand = new RelayCommand(
                async o =>
                {
                    SupplierPaymentsList.Clear();
                    if (!String.IsNullOrWhiteSpace(SearchText))
                    {
                        var suppliers = await SearchSupplierPaymentsAsync(SupplierId);

                        foreach (var newSupplier in suppliers)
                        {
                            SupplierPaymentsList.Add(new SupplierPaymentCardViewModel(navigationService, newSupplier));
                        }
                    }
                },
                o => !String.IsNullOrWhiteSpace(SearchText));
        }

        private async Task<List<SupplierPayment>> SearchSupplierPaymentsAsync(int supplierId)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(SearchText))
                {
                    var result = await _supplierPaymentRepository.GetPaymentsBySupplierIdAsync(supplierId);
                    if (result.FirstOrDefault().idProveedor == -1)
                    {
                        ErrorMessage = "No se encontraron empleados con los datos proporcionados";
                    }
                    else
                    {
                        ErrorMessage = string.Empty;
                        return ConvertToEmployeeList(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la búsqueda de empleados: {ex.Message}");
            }
            return new List<SupplierPayment>();
        }

        private List<SupplierPayment> ConvertToEmployeeList(List<CompraProveedor> list)
        {
            List<SupplierPayment> supplierPayments = new List<SupplierPayment>();

            foreach (var supplierPayment in list)
            {
                SupplierPayment newSupplierPayment = new SupplierPayment();
                newSupplierPayment.SupplierId = supplierPayment.idProveedor;
                newSupplierPayment.TotalAmount = supplierPayment.montoTotal;
                newSupplierPayment.Folio = supplierPayment.folio;
                newSupplierPayment.PurchaseDate = supplierPayment.fechaCompra;
                newSupplierPayment.AdministratorId = supplierPayment.idAdministrador;

                supplierPayments.Add(newSupplierPayment);
            }
            return supplierPayments;
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Supplier supplier)
            {
                ActualSupplier = supplier;
                _originalSupplier = (Supplier)supplier.Clone();

                InitProperties();

                SearchCommand.Execute(null);
            }
            else
            {
                MessageBox.Show("Error al cargar el empleado");
            }
        }

        private void InitProperties()
        {
            SupplierId = ActualSupplier.SupplierId;
            SupplierName = ActualSupplier.SupplierName;
            PrimaryContact = ActualSupplier.PrimaryContact;
            Phone = ActualSupplier.Phone;
            City = ActualSupplier.City;
        }
    }
}