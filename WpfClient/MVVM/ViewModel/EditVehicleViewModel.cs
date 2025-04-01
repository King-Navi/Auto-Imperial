using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities.Enum;
using WpfClient.Utilities;

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


        public ObservableCollection<string> BranchesList { get; set; }

        private string selectedBranch;
        public string SelectedBranch
        {
            get { return selectedBranch; }
            set
            {
                if (selectedBranch != value)
                {
                    selectedBranch = value;
                    OnPropertyChanged(nameof(SelectedBranch));
                }
            }
        }
        public ObservableCollection<string> ModelsList { get; set; }

        private string selectedModel;
        public string SelectedModel
        {
            get { return selectedModel; }
            set
            {
                if (selectedModel != value)
                {
                    selectedModel = value;
                    OnPropertyChanged(nameof(SelectedModel));
                }
            }
        }

        public ObservableCollection<string> VersionsList { get; set; }

        private string selectedVersion;
        public string SelectedVersion
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

        //private readonly IVehicleRepository _vehicleRepository;
        private readonly IDialogService _dialogService;


        public EditVehicleViewModel(INavigationService navigationService, UserService currentUser, IDialogService dialogService)//IVehicleRepository vehicleRepository)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            //_vehicleRepository = vehicleRepository;
            InicializateBranches();
            InicializateModels();
            InicializateVersions();
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
                //_originalVehicle = (Vehicle)vehicle.Clone();

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
            Doors = ActualVehicle.Doors;
            Engine = ActualVehicle.Engine;
        }
        private void InicializateBranches()
        {
            BranchesList = new ObservableCollection<string>
            {
                "Asesor de ventas",
                "Ejecutivo de ventas",
                "Asesor de posventa",
                "Especialista en financiamiento"
            };
        }

        private void InicializateModels()
        {
            ModelsList = new ObservableCollection<string>
            {
                "Asesor de ventas",
                "Ejecutivo de ventas",
                "Asesor de posventa",
                "Especialista en financiamiento"
            };
        }

        private void InicializateVersions()
        {
            VersionsList = new ObservableCollection<string>
            {
                "Asesor de ventas",
                "Ejecutivo de ventas",
                "Asesor de posventa",
                "Especialista en financiamiento"
            };
        }

        private void InicializateTypes()
        {
            TypesList = new ObservableCollection<string>
            {
                "Asesor de ventas",
                "Ejecutivo de ventas",
                "Asesor de posventa",
                "Especialista en financiamiento"
            };
        }

        private void NavigateToSearchVehicle()
        {
            var confirmationVM = new ConfirmationViewModel("Cancelar edición", $"¿Esta seguro que desea cancelar la edición? Se perderán todos los cambios no guardados", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Navigation.NavigateTo<SearchVehicleViewModel>();
            }
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

        private void EditVehicle()
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
