using AutoImperialDAO.DAO.Interfaces;
using Services.Dialogs;
using Services.Navigation;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class InfoSellViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private Sell _actualSell = new Sell();
        private Sell? _originalSell;

        public DateOnly Date { get; set; }
        public string SellPrice { get; set; }
        public string Seller { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAddress { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleVIN { get; set; }
        public string VehicleVersion { get; set; }
        public string SellNotes { get; set; }


        public Sell ActualSell
        {
            get => _actualSell;
            set
            {
                _actualSell = value;
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
        private readonly ISellRepository _sellRepository;


        public ICommand NavigateToSearchSellView { get; set; }
        public ICommand DeleteSellCommand { get; set; }
        public ICommand EditSellCommand { get; set; }
        public InfoSellViewModel(INavigationService navigationService, IDialogService dialogService, ISellRepository sellRepository)
        {
            _dialogService = dialogService;
            _sellRepository = sellRepository;
            Navigation = navigationService;


            NavigateToSearchSellView = new RelayCommand(NavigateToSearchSell);
            DeleteSellCommand = new RelayCommand(DeleteSell);
            EditSellCommand = new RelayCommand(EditSell);
        }
        public void ReceiveParameter(object parameter)
        {
            if (parameter is Sell sell)
            {
                ActualSell = sell;
                _originalSell = (Sell)sell.Clone();

                InitProperties();
            }
            else
            {
                MessageBox.Show("Error al cargar el empleado");
            }
        }

        private void NavigateToSearchSell()
        {
            Navigation.NavigateTo<SearchSellViewModel>();
        }

        private void EditSell()
        {
            Navigation.NavigateTo<EditSellViewModel>(ActualSell);
        }

        void DeleteSell()
        {
            var confirmationVM = new ConfirmationViewModel("Confirmación de eliminación", $"¿Desea eliminar este empleado?", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (false == result)
            {
                return;
            }
            DeleteSellOnDB(ActualSell.idVenta);
        }


        private void InitProperties()
        {
            Date = ActualSell.fechaVenta;
            SellPrice = ActualSell.precioVehiculo.HasValue
                            ? ActualSell.precioVehiculo.Value.ToString("C")
                            : "N/A";
            Seller = ActualSell.idReservaNavigation.idVendedorNavigation.nombre + " " +
                ActualSell.idReservaNavigation.idVendedorNavigation.apellidoPaterno;
            SellNotes = ActualSell.notasAdicionales;

            VehicleVIN = ActualSell.idVehiculoNavigation.VIN;
            VehicleBrand = ActualSell.idVehiculoNavigation
                                .idVersionNavigation
                                .idModeloNavigation
                                .idMarcaNavigation.nombre;
            VehicleModel = ActualSell.idVehiculoNavigation
                                .idVersionNavigation
                                .idModeloNavigation.nombre;
            VehicleVersion = ActualSell.idVehiculoNavigation
                                .idVersionNavigation.nombre;
            
            var cliente = ActualSell.idReservaNavigation.idClienteNavigation;
            ClientName = $"{cliente.nombre} {cliente.apellidoPaterno} {cliente.apellidoMaterno}";
            ClientPhone = cliente.telefono; 
            ClientAddress = $"{cliente.calle} {cliente.numero}, CP {cliente.codigoPostal}, {cliente.ciudad}";
        }

        private void DeleteSellOnDB(int sellId)
        {
            if (_sellRepository.DeleteById(sellId))
            {
                MessageBox.Show("Venta eliminada correctamente");
                Navigation.NavigateTo<SearchSellViewModel>();
            }
            else
            {
                MessageBox.Show("Error al eliminar la venta");
            }
        }
    }
}
