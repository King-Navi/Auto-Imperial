using Services.Navigation;
using WpfClient.Utilities;
using WpfClient.MVVM.Model;
using Services.Dialogs;
using System.Windows.Input;
using WpfClient.Utilities.Validation;
using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;

namespace WpfClient.MVVM.ViewModel
{
    internal class RegisterClientViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private readonly GenericComparer<Client> _clientComparer = new GenericComparer<Client>();

        private Client _clienteActual = new Client();
        private Client? _clienteOriginal;

        public Client ClienteActual
        {
            get => _clienteActual;
            set
            {
                _clienteActual = value;
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
        private readonly IClientRepository _clientRepository;

        public ICommand RegisterClientCommand { get; set; }

        public ICommand NavigateToSearchClientView { get; set; }
        public RegisterClientViewModel(INavigationService navigationService, IDialogService dialogService , IClientRepository clientRepository)
        {
            _dialogService = dialogService;
            _clientRepository = clientRepository;
            Navigation = navigationService;

            NavigateToSearchClientView = new RelayCommand(NavigateToSearchClient);
            RegisterClientCommand = new RelayCommand(RegisterClient, CanRegisterClient);
        }

        private void NavigateToSearchClient()
        {
            Navigation.NavigateTo<SearchClientViewModel>();
        }

        private void RegisterClient(object obj)
        {
            if (IsEditMode() && !HasChanges())
            {
                var alertNoChanges = new AlertViewModel($"No hay cambios para guardar");
                _dialogService.ShowDialog(alertNoChanges);
                return;
            }
            var confirmationVM = new ConfirmationViewModel($"¿Deseas registrar al cliente {ClienteActual.Name}?");
            var result = _dialogService.ShowDialog(confirmationVM);

            //HARDCODED
            //ClienteActual.Email = "juan@example.com";
            //ClienteActual.Phone = "1234567890";
            //ClienteActual.Street = "Calle 1";
            //ClienteActual.Number = 123;
            //ClienteActual.PaternalSurname = "P";
            //ClienteActual.MaternalSurname = "M";
            //ClienteActual.CP = "12345";
            //ClienteActual.City = "CDMX";
            //ClienteActual.RFC = "GOMJ850123MNE";
            //ClienteActual.CURP = "GOMJ850123HDFRLR05";
            //HARDCODED

            if (result == true )
            {
                var validationErrors = ValidateClient();
                if (validationErrors.Count > 0)
                {
                    var alertVM = new AlertViewModel("Errores de validación", validationErrors);
                    _ = _dialogService.ShowDialog(alertVM);
                    return;
                }

                if (SaveClientChanges())
                {
                    var alertVM = new AlertViewModel($"Se registrar al cliente {ClienteActual.Name}!!!");
                    _ = _dialogService.ShowDialog(alertVM);

                }
                else
                {
                    var errorVM = new AlertViewModel("Error al guardar el cliente.");
                    _ = _dialogService.ShowDialog(errorVM);
                }
            }
        }

        public bool SaveClientChanges()
        {
            try
            {
                if (IsEditMode())
                {
                    return _clientRepository.Edit(ClienteActual);
                }
                else
                {
                    return _clientRepository.Register(ClienteActual);
                }
            }
            catch (Exception ex)
            {
                //TODO: BD error
            }
            return false;

        }
        private bool IsEditMode()
        {
            return _clienteOriginal != null && _clienteOriginal.IdClient > 0;
        }
        private bool HasChanges()
        {
            return !_clientComparer.Equals(_clienteOriginal, ClienteActual);
        }
        private bool CanRegisterClient(object obj)
        {
            return ClienteActual != null;
        }

        private List<string> ValidateClient()
        {
            ClientValidator validator = new ClientValidator();
            var validationResult = validator.Validate(ClienteActual);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                                       .Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                                       .ToList();
            }

            return new List<string>();
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Client client)
            {
                ClienteActual = client;
                _clienteOriginal = (Client)client.Clone(); 
            }
        }
    }
}
