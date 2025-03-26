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
    class SupplierCardViewModel : Services.Navigation.ViewModel
    {
        private Supplier _supplier = new Supplier();

        public string SupplierName { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierPrimaryContact { get; set; }

        public Supplier Supplier
        {
            get => _supplier;
            set
            {
                _supplier = value;
                OnPropertyChanged();
            }
        }
        public ICommand NavigateToViewSupplierViewCommand { get; set; }

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

        public SupplierCardViewModel(INavigationService navigationService, Supplier supplier)
        {
            Supplier = supplier;
            SupplierName = supplier.SupplierName;
            SupplierCity = supplier.City ?? "Ciudad no especificada";
            SupplierPrimaryContact = "Contacto principal: " + supplier.PrimaryContact;
            Navigation = navigationService;
            NavigateToViewSupplierViewCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<InfoSupplierViewModel>(Supplier);
                },
                o => true);
        }

    }

}
