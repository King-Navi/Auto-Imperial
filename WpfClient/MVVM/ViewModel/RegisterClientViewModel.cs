using Services.Navigation;
using WpfClient.Utilities;
using WpfClient.MVVM.Model;
using Services.Dialogs;
using System.Windows.Input;
using WpfClient.Utilities.Validation;
using AutoImperialDAO.DAO.Interfaces;
using WpfClient.Idioms;
using System.Diagnostics;

namespace WpfClient.MVVM.ViewModel
{
    internal class RegisterClientViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private readonly GenericComparer<Client> _clientComparer = new GenericComparer<Client>();

        private Client _clienteActual = new Client();
        private Client? _clienteOriginal;
        private string _AfirmativeButton;
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
        private readonly IClientRepository _clientRepository;

        private readonly IDialogService _dialogService;

        public ICommand RegisterClientCommand { get; set; }

        public ICommand NavigateToSearchClientView { get; set; }
        public string AfirmativeButton
        {
            get => _AfirmativeButton;
            set
            {
                _AfirmativeButton = value;
                OnPropertyChanged();
            }
        }

        public RegisterClientViewModel(INavigationService navigationService, IDialogService dialogService, IClientRepository clientRepository)
        {
            DisposeData();
            _dialogService = dialogService;
            _clientRepository = clientRepository;
            Navigation = navigationService;
            NavigateToSearchClientView = new RelayCommand(NavigateToSearchClient);
            RegisterClientCommand = new RelayCommand(RegisterClient, CanRegisterClient);
        }

        private void DisposeData()
        {
            ClienteActual = new Client();
            _clienteOriginal = null;
            AfirmativeButton = Language.GetLocalizedString(TextKeys.Register_Client);
        }

        private void NavigateToSearchClient()
        {
            if (HasChanges())
            {
                var confirmCancel = new ConfirmationViewModel(TextKeys.Cancel_Edit, TextKeys.Discard_Changes, Utilities.Enum.ConfirmationIconType.WarningIcon);
                var result = _dialogService.ShowDialog(confirmCancel);

                if (result == false)
                    return;
            }

            DisposeData();
            Navigation.NavigateTo<SearchClientViewModel>();
        }

        private void RegisterClient(object obj)
        {
            if (IsEditMode() && !HasChanges())
            {
                var alertNoChanges = new AlertViewModel(TextKeys.No_Changes_Made, TextKeys.No_Changes_To_Save, Utilities.Enum.AlertIconType.AlertIcon);
                _dialogService.ShowDialog(alertNoChanges);
                return;
            }
            var confirmationVM = new ConfirmationViewModel(TextKeys.Confirmation, TextKeys.Confirm_Register_Client, Utilities.Enum.ConfirmationIconType.WarningIcon);
            bool? result = _dialogService.ShowDialog(confirmationVM);
            if (result != true)
            {
                return;
            }

            var validationErrors = ValidateClient();
            if (validationErrors.Count > 0)
            {
                var alertVM = new AlertViewModel(TextKeys.Validation_Errors, TextKeys.Validation_Errors, Utilities.Enum.AlertIconType.AlertIcon, validationErrors);
                _ = _dialogService.ShowDialog(alertVM);
                return;
            }

            if (SaveClientChanges())
            {
                var alertVM = new AlertViewModel(TextKeys.Client_Registered, TextKeys.Client_Will_Be_Registered, Utilities.Enum.AlertIconType.AlertIcon);
                _ = _dialogService.ShowDialog(alertVM);
                DisposeData();
                Navigation.NavigateTo<SearchClientViewModel>();
                return;
            }
            else
            {
                var errorVM = new AlertViewModel(TextKeys.Save_Error, TextKeys.Save_Error, Utilities.Enum.AlertIconType.AlertIcon);
                _ = _dialogService.ShowDialog(errorVM);
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
            if (_clienteOriginal == null)
                return !_clientComparer.Equals(new Client(), ClienteActual);

            return !_clientComparer.Equals(_clienteOriginal, ClienteActual);
        }
        [DebuggerStepThrough]
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
                AfirmativeButton = Language.GetLocalizedString(TextKeys.Edit_Client);
            }
            else
            {
                DisposeData();
            }
        }

        public bool? ShowDialog(object viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
