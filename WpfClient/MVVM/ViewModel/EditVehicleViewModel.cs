using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities.Enum;
using WpfClient.Utilities;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace WpfClient.MVVM.ViewModel
{
    class EditVehicleViewModel : Services.Navigation.ViewModel, IParameterReceiver
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
                    if (!isInit)
                    {
                        _ = LoadModelsAsync(value);
                    }
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
                    if (!isInit)
                    {
                        _ = LoadVersionsAsync(selectedModel);
                    }
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



        private Vehicle _actualVehicle = new Vehicle();
        private Vehicle? _originalVehicle;
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

        public ICommand NavigateToSearchVehicleView { get; set; }
        public ICommand EditVehicleCommand { get; set; }
        public ICommand UploadPhotoCommand { get; set; }

        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDialogService _dialogService;
        private bool isInit = true;


        public EditVehicleViewModel(INavigationService navigationService, UserService currentUser, IDialogService dialogService, IVehicleRepository vehicleRepository)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _vehicleRepository = vehicleRepository;
            InicializateTypes();
            NavigateToSearchVehicleView = new RelayCommand(NavigateToSearchVehicle);
            EditVehicleCommand = new RelayCommand(EditVehicle);
            UploadPhotoCommand = new RelayCommand(UploadPhoto);
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
        private void InitProperties()
        {
            Color = ActualVehicle.Color;
            Transmission = ActualVehicle.Transmission;
            ChassisNumber = ActualVehicle.ChassisNumber;
            EngineNumber = ActualVehicle.EngineNumber;
            VIN = ActualVehicle.VIN;
            Year = ActualVehicle.Year.ToString();
            PurchasePrice = ActualVehicle.SupplierPrice.ToString();
            SellPrice = ActualVehicle.SellPrice.ToString();
            VehiclePhoto = ActualVehicle.Photos.FirstOrDefault().foto;
            LoadPhoto();
            SelectedType = ActualVehicle.VehicleType;

            _ = LoadCombos();
        }

        private async Task LoadCombos()
        {
            if (BranchesList == null || BranchesList.Count == 0)
                await InicializateBranchesAsync();

            var selectedBranch = BranchesList.FirstOrDefault(b => b.idMarca == ActualVehicle.IdBranch);
            if (selectedBranch != null)
            {
                SelectedBranch = selectedBranch;
                await LoadModelsAsync(selectedBranch);

                var selectedModel = ModelsList.FirstOrDefault(m => m.idModelo == ActualVehicle.IdModel);
                if (selectedModel != null)
                {
                    SelectedModel = selectedModel;
                    await LoadVersionsAsync(selectedModel);

                    var selectedVersion = VersionsList.FirstOrDefault(v => v.idVersion == ActualVehicle.IdVersion);
                    if (selectedVersion != null)
                    {
                        SelectedVersion = selectedVersion;
                    }
                }
            }
            isInit = false;
        }

        private void LoadPhoto()
        {
            if (VehiclePhoto == null)
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

        private async Task InicializateBranchesAsync()
        {
            try
            {
                var branches = await _vehicleRepository.GetAllBranchAsync();
                branches = branches.OrderBy(m => m.nombre).ToList();
                BranchesList = new ObservableCollection<Marca>();
                OnPropertyChanged(nameof(BranchesList));
                BranchesList.Clear();

                foreach (var branch in branches)
                {
                    BranchesList.Add(branch);
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
                var models = await _vehicleRepository.GetModelsByBrandIdAsync(branch.idMarca);
                models = models.OrderBy(m => m.nombre).ToList();
                if (models?.Any() == true)
                {
                    foreach (var model in models)
                    {
                        ModelsList.Add(model);
                    }
                    IsModelEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar modelos: {ex.Message}");
            }
        }

        private async Task LoadVersionsAsync(Modelo model)
        {
            VersionsList = new ObservableCollection<AutoImperialDAO.Models.Version>();
            OnPropertyChanged(nameof(VersionsList));
            IsVersionEnabled = false;

            if (model == null) return;

            try
            {
                var versions = await _vehicleRepository.GetVersionsByModelIdAsync(model.idModelo);
                versions = versions.OrderBy(v => v.nombre).ToList();
                if (versions?.Any() == true)
                {
                    foreach (var version in versions)
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

        private void NavigateToSearchVehicle()
        {
            var confirmationVM = new ConfirmationViewModel("Cancelar edición", $"¿Esta seguro que desea cancelar la edición? Se perderán todos los cambios no guardados", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                var searchVM = App.ServiceProvider.GetRequiredService<SearchVehicleViewModel>();
                searchVM.ResetSearch();
                Navigation.NavigateTo<SearchVehicleViewModel>();
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(Color) ||
                string.IsNullOrWhiteSpace(ChassisNumber) ||
                string.IsNullOrWhiteSpace(EngineNumber) ||
                string.IsNullOrWhiteSpace(VIN) ||
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

            if (!decimal.TryParse(SellPrice, out _))
            {
                _dialogService.ShowDialog(new AlertViewModel("Precio inválido", "Introduce un precio de venta válido.", AlertIconType.AlertIcon));
                return false;
            }

            if (!int.TryParse(Year, out int parsedYear) || parsedYear < 1900 || parsedYear > DateTime.Now.Year + 1)
            {
                _dialogService.ShowDialog(new AlertViewModel("Año inválido", "Introduce un año válido.", AlertIconType.AlertIcon));
                return false;
            }

            return true;
        }

        private async void EditVehicle()
        {
            if (!ValidateFields())
                return;

            var confirmationVM = new ConfirmationViewModel("Confirmación de edición", $"¿Deseas guardar los cambios en el vehículo?", ConfirmationIconType.RegisterIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Vehiculo editedVehicle = new Vehiculo
                {
                    idVehiculo = ActualVehicle.VehicleId,
                    tipoVehiculo = SelectedType,
                    estadoVehiculo = ActualVehicle.VehicleStatus,
                    precioProveedor = ActualVehicle.SupplierPrice,
                    precioVehiculo = decimal.Parse(SellPrice),
                    anio = int.Parse(Year),
                    color = Color,
                    VIN = VIN,
                    numeroChasis = ChassisNumber,
                    numeroMotor = EngineNumber,
                    idVersion = SelectedVersion.idVersion,
                    idCompraProveedor = ActualVehicle.SupplierPurchaseName
                };

                if (VehiclePhoto != null)
                {
                    editedVehicle.Fotos.Add(new Fotos
                    {
                        foto = VehiclePhoto
                    });
                }

                try
                {
                    var success = await _vehicleRepository.EditVehicleAsync(editedVehicle);
                    if (success)
                    {
                        MessageBox.Show("Vehículo editado correctamente");
                        var searchVM = App.ServiceProvider.GetRequiredService<SearchVehicleViewModel>();
                        searchVM.ResetSearch();
                        Navigation.NavigateTo<SearchVehicleViewModel>();
                    }
                    else
                    {
                        MessageBox.Show("Error al editar el vehículo");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inesperado al editar el vehículo: {ex.Message}");
                }
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
