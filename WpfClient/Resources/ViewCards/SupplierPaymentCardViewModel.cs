using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.ViewModel;
using WpfClient.Utilities;

namespace WpfClient.Resources.ViewCards
{
    class SupplierPaymentCardViewModel : Services.Navigation.ViewModel
    {
        private SupplierPayment _supplierPayment = new SupplierPayment();

        public string SupplierPaymentFolio { get; set; }
        public string SupplierPaymentDate { get; set; }
        public string SupplierPaymentAmount { get; set; }

        public SupplierPayment SupplierPayment
        {
            get => _supplierPayment;
            set
            {
                _supplierPayment = value;
                OnPropertyChanged();
            }
        }
        public ICommand NavigateToViewSupplierPaymentViewCommand { get; set; }

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

        public SupplierPaymentCardViewModel(INavigationService navigationService, SupplierPayment supplier)
        {
            SupplierPayment = supplier;
            SupplierPaymentFolio = "Folio: " + supplier.Folio;
            SupplierPaymentDate = "Fecha: " + supplier.PurchaseDate.ToString("yyyy‑MM‑dd");
            SupplierPaymentAmount = "Monto total: " + supplier.TotalAmount?.ToString("N2") ?? "0.00";
            Navigation = navigationService;
            NavigateToViewSupplierPaymentViewCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<InfoSupplierPaymentViewModel>(SupplierPayment);
                },
                o => true);
        }

    }

}
