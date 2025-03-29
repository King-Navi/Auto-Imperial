using AutoImperialDAO.DAO.Interfaces;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfClient.Idioms;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using WpfClient.Utilities.Validation;

namespace WpfClient.MVVM.ViewModel
{
    public class RegisterReserveViewModel : Services.Navigation.ViewModel, ICloseable, IParameterReceiver
    {
        private const int NO_ERRORS = 0;
        private const int SUCCESS_SAVE = 1;

        private Client _currentClient = new Client();

        public Client CurrentClient
        {
            get => _currentClient;
            set { _currentClient = value; OnPropertyChanged(); }
        }
        private SellerEmployee _currentEmployee = new SellerEmployee();

        public SellerEmployee CurrentEmployee
        {
            get => _currentEmployee;
            set { _currentEmployee = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Brand> _marcas;
        public ObservableCollection<Brand> Marcas
        {
            get => _marcas;
            set { _marcas = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ModelBase> _models;
        public ObservableCollection<ModelBase> Models
        {
            get => _models;
            set { _models = value; OnPropertyChanged(); }
        }

        private ObservableCollection<VersionModel> _versions;
        public ObservableCollection<VersionModel> Versions
        {
            get => _versions;
            set { _versions = value; OnPropertyChanged(); }
        }

        private Brand _selectedBrand;
        public Brand SelectedBrand
        {
            get => _selectedBrand;
            set
            {
                _selectedBrand = value;
                OnPropertyChanged();
                LoadModelos();
            }
        }

        private ModelBase _selectedModel;
        public ModelBase SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                OnPropertyChanged();
                LoadVersiones();
            }
        }

        private VersionModel _versionSeleccionada;
        public VersionModel VersionSeleccionada
        {
            get => _versionSeleccionada;
            set { _versionSeleccionada = value; OnPropertyChanged(); }
        }
        private int _downPayment = 0;
        public int DownPayment
        {
            get => _downPayment;
            set { _downPayment = value; OnPropertyChanged(); }
        }

        private string _additionalNotes;

        public string AdditionalNotes
        {
            get => _additionalNotes;
            set { _additionalNotes = value; OnPropertyChanged(); }
        }

        public event Action<bool?> CloseRequested;
        private readonly IBrandRepository _brand;
        private readonly IReserveRepository _reserve;
        private readonly IDialogService _dialogService;
        private readonly ICollectionUpdater _collectionObserver;
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public RegisterReserveViewModel(SellerEmployee employee, IBrandRepository brand, IReserveRepository reserve, IDialogService dialogService , ICollectionUpdater eventObserver)
        {
            _collectionObserver = eventObserver;
            _dialogService = dialogService;
            _reserve = reserve;
            _brand = brand;
            CurrentEmployee = employee;
            LoadBrands();
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(false));
            _reserve = reserve;
        }

        private void Confirm() //FIXME : This method is too long, consider refactoring
        {
            var validationErrors = new List<string>();

            if (CurrentClient == null || CurrentClient.IdClient <= 0)
                validationErrors.Add(" Debe seleccionar un cliente válido. ");

            if (CurrentEmployee == null || CurrentEmployee.IdEmployee <= 0)
                validationErrors.Add(" Debe seleccionar un empleado válido. ");

            if (VersionSeleccionada == null || VersionSeleccionada.IdVersion <= 0)
                validationErrors.Add(" Debe seleccionar una versión válida. ");

            if (validationErrors.Any())
            {
                var alertVM = new AlertViewModel(
                    TextKeys.Validation_Errors,
                    TextKeys.Validation_Errors,
                    Utilities.Enum.AlertIconType.AlertIcon,
                    validationErrors
                );
                _ = _dialogService.ShowDialog(alertVM);
                return;
            }

            var reserve = new Reserve
            {
                IdClient = CurrentClient.IdClient,
                IdSeller = CurrentEmployee.IdEmployee,
                IdVersion = VersionSeleccionada.IdVersion,
                DownPayment = this.DownPayment,
                AdditionalNotes = AdditionalNotes,
                ReserveDate = DateTime.Now
            };

            var validatorErrors = ValidateReserve(reserve);
            if (validatorErrors.Any())
            {
                var alertVM = new AlertViewModel(
                    TextKeys.Validation_Errors,
                    TextKeys.Validation_Errors,
                    Utilities.Enum.AlertIconType.AlertIcon,
                    validatorErrors
                );
                _ = _dialogService.ShowDialog(alertVM);
                return;
            }

            int result = SaveReserve(reserve);
            if (result != SUCCESS_SAVE)
            {
                var alertVM = new AlertViewModel(
                    TextKeys.Save_Error,
                    TextKeys.Save_Error,
                    Utilities.Enum.AlertIconType.AlertIcon
                );
                _ = _dialogService.ShowDialog(alertVM);
                return;
            }

            CloseRequested?.Invoke(true);
            _collectionObserver.UpdateCollection();
        }

        public int SaveReserve(Reserve reserve)
        {
           return _reserve.CreateReserve(reserve);
        }
        private List<string> ValidateReserve(Reserve reserve)
        {
            ReserveValidator validator = new ReserveValidator();
            var validationResult = validator.Validate(reserve);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                                       .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                                       .ToList();
            }

            return new List<string>();
        }

        private void LoadModelos()
        {

            if (SelectedBrand != null)
            {
                Models = new ObservableCollection<ModelBase>(SelectedBrand.Models);
                SelectedModel = null;
                Versions = new ObservableCollection<VersionModel>();
            }
        }

        private void LoadVersiones()
        {
            if (SelectedModel != null)
            {
                Versions = new ObservableCollection<VersionModel>(
                    SelectedModel.Versions.Select(v => new VersionModel(v))
                );
                VersionSeleccionada = null;
            }
        }
        public void LoadBrands()
        {
            var marcasFromDb = _brand.GetAllBrandsWithModelsAndVersions();
            var brands = marcasFromDb.Select(m => new Brand(m)).ToList();
            Marcas = new ObservableCollection<Brand>(brands);
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Client client)
            {
                CurrentClient = client;
            }
            else
            {
                CurrentClient = null;
            }
        }
    }
}
