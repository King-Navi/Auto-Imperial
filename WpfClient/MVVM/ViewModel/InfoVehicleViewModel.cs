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
using System.Windows.Media.Imaging;
using System.IO;
using WpfClient.Utilities.Enum;
using Microsoft.Extensions.DependencyInjection;

namespace WpfClient.MVVM.ViewModel
{
    class InfoVehicleViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private Vehicle _actualVehicle = new Vehicle();
        private Vehicle? _originalVehicle;

        public string Branch { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string Color { get; set; }
        public string Transmission { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string VIN { get; set; }
        public string Discount { get; set; }
        public string Year { get; set; }
        public string PurchasePrice { get; set; }
        public string SellPrice { get; set; }
        public string Type { get; set; }
        public string Doors { get; set; }
        public string Engine { get; set; }

        private byte[]? vehiclePhoto;
        public byte[]? VehiclePhoto
        {
            get => vehiclePhoto;
            set
            {
                vehiclePhoto = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage? previewImage;
        public BitmapImage? PreviewImage
        {
            get => previewImage;
            set
            {
                previewImage = value;
                OnPropertyChanged();
            }
        }


        public Vehicle ActualVehicle
        {
            get => _actualVehicle;
            set
            {
                _actualVehicle = value;
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
        private readonly IVehicleRepository _vehicleRepository;


        public ICommand NavigateToSearchVehicleView { get; set; }
        public ICommand DeleteVehicleCommand { get; set; }
        public ICommand EditVehicleCommand { get; set; }
        public InfoVehicleViewModel(INavigationService navigationService, IDialogService dialogService, IVehicleRepository vehicleRepository)
        {
            _dialogService = dialogService;
            _vehicleRepository = vehicleRepository;
            Navigation = navigationService;


            NavigateToSearchVehicleView = new RelayCommand(NavigateToSearchVehicle);
            DeleteVehicleCommand = new RelayCommand(DeleteVehicle);
            EditVehicleCommand = new RelayCommand(EditVehicle);
        }
        public void ReceiveParameter(object parameter)
        {
            if (parameter is Vehicle vehicle)
            {
                ActualVehicle = vehicle;

                InitProperties();
            }
            else
            {
                MessageBox.Show("Error al cargar el vehículo");
            }
        }

        private void NavigateToSearchVehicle()
        {
            var searchVM = App.ServiceProvider.GetRequiredService<SearchVehicleViewModel>();
            searchVM.ResetSearch();
            Navigation.NavigateTo<SearchVehicleViewModel>();
        }

        private void EditVehicle()
        {
            Navigation.NavigateTo<EditVehicleViewModel>(ActualVehicle);
        }

        void DeleteVehicle() 
        {
            var confirmationVM = new ConfirmationViewModel("Confimracion de eliminación", $"¿Desea eliminar este vehículo?", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (false == result)
            {
                return;
            }
            DeleteVehicleOnDB(ActualVehicle.VehicleId);
        }


        private void InitProperties()
        {
            Branch = ActualVehicle.Branch;
            Model = ActualVehicle.Model;
            Version = ActualVehicle.Version;
            Color = ActualVehicle.Color;
            Transmission = ActualVehicle.Transmission;
            ChassisNumber = ActualVehicle.ChassisNumber;
            EngineNumber = ActualVehicle.EngineNumber;
            VIN = ActualVehicle.VIN;
            Discount = ActualVehicle.Discounts.FirstOrDefault();
            Year = ActualVehicle.Year.ToString();
            PurchasePrice = ActualVehicle.SupplierPrice.ToString();
            SellPrice = ActualVehicle.SellPrice.ToString();
            Type = ActualVehicle.VehicleType;
            Doors = ActualVehicle.Doors;
            Engine = ActualVehicle.Engine;
            VehiclePhoto = ActualVehicle.Photos.FirstOrDefault().foto;
            LoadPhoto();
        }

        private void LoadPhoto()
        {
            if(VehiclePhoto == null)
            {
                return;
            }

            using var stream = new MemoryStream(VehiclePhoto);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();

            PreviewImage = image;
        }

        private void DeleteVehicleOnDB(int IdVehicle)
        {
            if (_vehicleRepository.DeleteById(IdVehicle))
            {
                MessageBox.Show("Vehículo eliminado correctamente");
                var searchVM = App.ServiceProvider.GetRequiredService<SearchVehicleViewModel>();
                searchVM.ResetSearch();
                Navigation.NavigateTo<SearchVehicleViewModel>();
            }
            else
            {
                MessageBox.Show("Error al eliminar el vehículo");
            }
        }




    }
}
