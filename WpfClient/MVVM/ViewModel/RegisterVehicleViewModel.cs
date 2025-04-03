using Services.Dialogs;
using Services.Navigation;
using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
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
using WpfClient.Utilities.Enum;
using System.Windows.Media.Imaging;
using System.Reflection.Metadata;

namespace WpfClient.MVVM.ViewModel
{
    public class RegisterVehicleViewModel : Services.Navigation.ViewModel
    {
        private string color;
        public string Color
        {
            get => color;
            set { color = value; OnPropertyChanged(); }
        }
        private string transmission;
        public string Transmission
        {
            get => transmission;
            set { transmission = value; OnPropertyChanged(); }
        }
        private string chassisNumber;
        public string ChassisNumber
        {
            get => chassisNumber;
            set { chassisNumber = value; OnPropertyChanged(); }
        }
        private string engineNumber;
        public string EngineNumber
        {
            get => engineNumber;
            set { engineNumber = value; OnPropertyChanged(); }
        }
        private string vin;
        public string VIN
        {
            get => vin;
            set { vin = value; OnPropertyChanged(); }
        }
        private string purchasePrice;
        public string PurchasePrice
        {
            get => purchasePrice;
            set { purchasePrice = value; OnPropertyChanged(); }
        }
        private string year;
        public string Year
        {
            get => year;
            set { year = value; OnPropertyChanged(); }
        }
        private string sellPrice;
        public string SellPrice
        {
            get => sellPrice;
            set { sellPrice = value; OnPropertyChanged(); }
        }
        private string doors;
        public string Doors
        {
            get => doors;
            set { doors = value; OnPropertyChanged(); }
        }
        private string engine;
        public string Engine
        {
            get => engine;
            set { engine = value; OnPropertyChanged(); }
        }
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

        private bool isModelEnabled;
        public bool IsModelEnabled
        {
            get => isModelEnabled;
            set
            {
                isModelEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool isVersionEnabled;
        public bool IsVersionEnabled
        {
            get => isVersionEnabled;
            set
            {
                isVersionEnabled = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Marca> BranchesList { get; set; }

        private Marca selectedBranch;
        public Marca SelectedBranch
        {
            get { return selectedBranch; }
            set
            {
                if (selectedBranch != value)
                {
                    selectedBranch = value;
                    OnPropertyChanged(nameof(SelectedBranch));
                    _ = LoadModelsAsync(value);
                }
            }
        }
        public ObservableCollection<Modelo> ModelsList { get; set; }

        private Modelo selectedModel;
        public Modelo SelectedModel
        {
            get { return selectedModel; }
            set
            {
                if (selectedModel != value)
                {
                    selectedModel = value;
                    OnPropertyChanged(nameof(SelectedModel));
                    _ = LoadVersionsAsync(selectedModel); 
                }
            }
        }

        public ObservableCollection<AutoImperialDAO.Models.Version> VersionsList { get; set; }

        private AutoImperialDAO.Models.Version selectedVersion;
        public AutoImperialDAO.Models.Version SelectedVersion
        {
            get { return selectedVersion; }
            set
            {
                if (selectedVersion != value)
                {
                    selectedVersion = value;
                    OnPropertyChanged(nameof(SelectedVersion));
                }
            }
        }

        public ObservableCollection<string> TypesList { get; set; }

        private string selectedType;
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged(nameof(SelectedType));
                }
            }
        }



        private int _actualIdSupplierPayment;
        public int ActualIdSupplierPayment
        {
            get => _actualIdSupplierPayment;
            set
            {
                _actualIdSupplierPayment = value;
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

        public ICommand NavigateToSearchVehicleView { get; set; }
        public ICommand RegisterVehicleCommand { get; set; }
        public ICommand UploadPhotoCommand { get; set; }

        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDialogService _dialogService;


        public RegisterVehicleViewModel(INavigationService navigationService, UserService currentUser, IDialogService dialogService, IVehicleRepository vehicleRepository, int idSupplierPayment)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _vehicleRepository = vehicleRepository;
            ActualIdSupplierPayment = idSupplierPayment;
            _ = InicializateBranchesAsync();
            InicializateTypes();
            RegisterVehicleCommand = new RelayCommand(RegisterVehicle);
            UploadPhotoCommand = new RelayCommand(UploadPhoto);
        }


        private async Task InicializateBranchesAsync()
        {
            try
            {
                var marcas = await _vehicleRepository.GetAllBranchAsync();
                marcas = marcas.OrderBy(m => m.nombre).ToList();
                BranchesList = new ObservableCollection<Marca>();
                OnPropertyChanged(nameof(BranchesList));
                BranchesList.Clear();

                foreach (var marca in marcas)
                {
                    BranchesList.Add(marca);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar marcas: {ex.Message}");
            }
        }

        private async Task LoadModelsAsync(Marca branch)
        {
            ModelsList = new ObservableCollection<Modelo>();
            
            OnPropertyChanged(nameof(ModelsList));
            IsModelEnabled = false;

            VersionsList = new ObservableCollection<AutoImperialDAO.Models.Version>();
            OnPropertyChanged(nameof(VersionsList));
            SelectedVersion = null;
            IsVersionEnabled = false;

            if (branch == null) return;

            try
            {
                var modelos = await _vehicleRepository.GetModelsByBrandIdAsync(branch.idMarca);
                modelos = modelos.OrderBy(m => m.nombre).ToList();
                if (modelos?.Any() == true)
                {
                    foreach (var modelo in modelos)
                    {
                        ModelsList.Add(modelo);
                    }
                    IsModelEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar modelos: {ex.Message}");
            }
        }

        private async Task LoadVersionsAsync(Modelo modelo)
        {
            VersionsList = new ObservableCollection<AutoImperialDAO.Models.Version>();
            OnPropertyChanged(nameof(VersionsList));
            IsVersionEnabled = false;

            if (modelo == null) return;

            try
            {
                var versiones = await _vehicleRepository.GetVersionsByModelIdAsync(modelo.idModelo);
                versiones = versiones.OrderBy(v => v.nombre).ToList();
                if (versiones?.Any() == true)
                {
                    foreach (var version in versiones)
                    {
                        VersionsList.Add(version);
                    }
                    IsVersionEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar versiones: {ex.Message}");
            }
        }

        private void InicializateTypes()
        {
            TypesList = new ObservableCollection<string>
            {
                "Automovil",
                "SUV",
                "Caminón",
                "Motocicleta",
                "Sedan"
            };
        }


        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(Color) ||
                string.IsNullOrWhiteSpace(ChassisNumber) ||
                string.IsNullOrWhiteSpace(EngineNumber) ||
                string.IsNullOrWhiteSpace(VIN) ||
                string.IsNullOrWhiteSpace(PurchasePrice) ||
                string.IsNullOrWhiteSpace(SellPrice) ||
                string.IsNullOrWhiteSpace(Year) ||
                SelectedBranch == null ||
                SelectedModel == null ||
                SelectedVersion == null ||
                string.IsNullOrWhiteSpace(SelectedType))
            {
                _dialogService.ShowDialog(new AlertViewModel("Campos incompletos", "Por favor, completa todos los campos requeridos.", AlertIconType.AlertIcon));
                return false;
            }

            if (!decimal.TryParse(PurchasePrice, out _) || !decimal.TryParse(SellPrice, out _))
            {
                _dialogService.ShowDialog(new AlertViewModel("Precios inválidos", "Introduce precios válidos para proveedor y venta.", AlertIconType.AlertIcon));
                return false;
            }

            if (!int.TryParse(Year, out int parsedYear) || parsedYear < 1900 || parsedYear > DateTime.Now.Year + 1)
            {
                _dialogService.ShowDialog(new AlertViewModel("Año inválido", "Introduce un año válido.", AlertIconType.AlertIcon));
                return false;
            }

            return true;
        }

        private async void RegisterVehicle(object parameter)
        {
            if (!ValidateFields())
                return;

            var nuevoVehiculo = new Vehiculo
            {
                tipoVehiculo = SelectedType,
                estadoVehiculo = "Disponible",
                precioProveedor = decimal.Parse(PurchasePrice),
                precioVehiculo = decimal.Parse(SellPrice),
                anio = int.Parse(Year),
                color = Color,
                VIN = VIN,
                numeroChasis = ChassisNumber,
                numeroMotor = EngineNumber,
                idVersion = SelectedVersion.idVersion,
                idCompraProveedor = _actualIdSupplierPayment
            };

            if (VehiclePhoto != null)
            {
                var foto = new Fotos
                {
                    foto = VehiclePhoto
                };

                nuevoVehiculo.Fotos.Add(foto);
            }

            try
            {
                bool result = await _vehicleRepository.RegisterVehicleAsync(nuevoVehiculo);
                if (result)
                {
                    MessageBox.Show("Vehículo registrado correctamente");

                    Mediator.Notify(MediatorKeys.UPDATE_VEHICLES_REGISTER, null);

                    if (parameter is Window window)
                    {
                        window.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Error al registrar el vehículo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al registrar el vehículo: {ex.Message}");
            }
        }

        public void UploadPhoto()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                FileInfo fileInfo = new FileInfo(filePath);
                const long MaxSizeBytes = 30 * 1024 * 1024;

                if (fileInfo.Length > MaxSizeBytes)
                {
                    _dialogService.ShowDialog(new AlertViewModel(
                        "Archivo demasiado grande",
                        "La imagen seleccionada excede los 30MB permitidos.",
                        AlertIconType.AlertIcon));
                    return;
                }

                try
                {
                    VehiclePhoto = File.ReadAllBytes(filePath);

                    using var stream = new MemoryStream(VehiclePhoto);
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();

                    PreviewImage = image;

                }
                catch (Exception ex)
                {
                    _dialogService.ShowDialog(new AlertViewModel(
                        "Error",
                        $"No se pudo cargar la imagen: {ex.Message}",
                        AlertIconType.AlertIcon));
                }
            }
        }

    }
}
