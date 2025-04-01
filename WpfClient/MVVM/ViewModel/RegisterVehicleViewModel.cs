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



        private Supplier _actualSupplier = new Supplier();
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

        public ICommand NavigateToSearchVehicleView { get; set; }
        public ICommand RegisterVehicleCommand { get; set; }
        public ICommand UploadPhotoCommand { get; set; }

        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDialogService _dialogService;


        public RegisterVehicleViewModel(INavigationService navigationService, UserService currentUser, IDialogService dialogService, IVehicleRepository vehicleRepository, Supplier supplier)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _vehicleRepository = vehicleRepository;
            ActualSupplier = supplier;
            _ = InicializateBranchesAsync();
            //InicializateModels();
            //InicializateVersions();
            InicializateTypes();
            RegisterVehicleCommand = new RelayCommand(RegisterVehicle);
            UploadPhotoCommand = new RelayCommand(UploadPhoto);
        }


        private async Task InicializateBranchesAsync()
        {
            try
            {
                var marcas = await _vehicleRepository.GetAllBranchAsync();
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

            // Limpia las versiones al cambiar de marca
            VersionsList = new ObservableCollection<AutoImperialDAO.Models.Version>();
            OnPropertyChanged(nameof(VersionsList));
            SelectedVersion = null;
            IsVersionEnabled = false;

            if (branch == null) return;

            try
            {
                var modelos = await _vehicleRepository.GetModelsByBrandIdAsync(branch.idMarca);
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
                "Motocicleta"
            };
        }


        //private bool ValidateFields()
        //{
        //    if (string.IsNullOrWhiteSpace(EmployeeName) ||
        //        string.IsNullOrWhiteSpace(PaternalSurname) ||
        //        string.IsNullOrWhiteSpace(MaternalSurname) ||
        //        string.IsNullOrWhiteSpace(Street) ||
        //        string.IsNullOrWhiteSpace(Number) ||
        //        string.IsNullOrWhiteSpace(CP) ||
        //        string.IsNullOrWhiteSpace(City) ||
        //        string.IsNullOrWhiteSpace(Phone) ||
        //        string.IsNullOrWhiteSpace(Mail) ||
        //        string.IsNullOrWhiteSpace(Curp) ||
        //        string.IsNullOrWhiteSpace(RFC) ||
        //        string.IsNullOrWhiteSpace(EmployeeNumber) ||
        //        string.IsNullOrWhiteSpace(Branch) ||
        //        string.IsNullOrWhiteSpace(Username) ||
        //        string.IsNullOrWhiteSpace(SelectedOption))
        //    {
        //        _dialogService.ShowDialog(new AlertViewModel("Campos vacíos", "Se han dejado campos vacíos, todos los campos son obligatorios.", AlertIconType.AlertIcon));
        //        return false;
        //    }

        //    if (!Regex.IsMatch(Mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        //    {
        //        _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un correo no válido", AlertIconType.AlertIcon));
        //        return false;
        //    }

        //    if (!Regex.IsMatch(Curp, @"^[A-Z]{4}\d{6}[A-Z]{6}\d{2}$", RegexOptions.IgnoreCase))
        //    {
        //        _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un CURP no válido", AlertIconType.AlertIcon));
        //        return false;
        //    }

        //    if (!Regex.IsMatch(RFC, @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$", RegexOptions.IgnoreCase))
        //    {
        //        _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un RFC no válido", AlertIconType.AlertIcon));
        //        return false;
        //    }

        //    if (!Regex.IsMatch(CP, @"^\d{5}$"))
        //    {
        //        _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un código postal no válido", AlertIconType.AlertIcon));
        //        return false;
        //    }

        //    if (!Regex.IsMatch(Phone, @"^\d{10}$"))
        //    {
        //        _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un número de teléfono no válido, debe de tener 10 dígitos", AlertIconType.AlertIcon));
        //        return false;
        //    }

        //    if (!int.TryParse(Number, out _))
        //    {
        //        _dialogService.ShowDialog(new AlertViewModel("Datos inválidos", "Se ha introducido un número de calle no válido", AlertIconType.AlertIcon));
        //        return false;
        //    }


        //    return true;
        //}

        private void RegisterVehicle()
        {
            //    if (!ValidateFields())
            //        return;

            //    var confirmationVM = new ConfirmationViewModel("Confimracion de registro", $"¿Esta seguro que desea guardar los cambios de este vehículo?", Utilities.Enum.ConfirmationIconType.RegisterIcon);
            //    var result = _dialogService.ShowDialog(confirmationVM);
            //    if (result == true)
            //    {
            //        Vendedor employee = new Vendedor
            //        {
            //            nombre = this.EmployeeName,
            //            apellidoPaterno = this.PaternalSurname,
            //            apellidoMaterno = this.MaternalSurname,
            //            calle = this.Street,
            //            numero = int.TryParse(this.Number, out int parsedNumber) ? parsedNumber : 0,
            //            codigoPostal = this.CP,
            //            ciudad = this.City,
            //            telefono = this.Phone,
            //            correo = this.Mail,
            //            CURP = this.Curp,
            //            RFC = this.RFC,
            //            numeroEmpleado = this.EmployeeNumber,
            //            sucursal = this.Branch,
            //            nombreUsuario = this.Username,
            //            puestoVendedor = this.SelectedOption,
            //            idVendedor = ActualEmployee.IdEmployee,
            //            Reservas = ActualEmployee.Reservas
            //        };



            //        try
            //        {
            //            if (_vehicleRepository.Edit(vehicle))
            //            {
            //                MessageBox.Show("Vehículo editado correctamente");
            //                Navigation.NavigateTo<SearchVehicleViewModel>();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Error al editar el vehículo");

            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show("Error al editar el vehículo: " + e.StackTrace);
            //        }
            //    }
        }

        public void UploadPhoto()
        {
            //var dialog = new OpenFileDialog();
            //dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            //if (dialog.ShowDialog() == true)
            //{
            //    ActualEmployee.Photo = dialog.FileName;
            //    OnPropertyChanged(nameof(ActualEmployee));
            //}
        }

    }
}
