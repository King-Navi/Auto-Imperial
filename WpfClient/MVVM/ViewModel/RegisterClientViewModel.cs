using Services.Navigation;
using WpfClient.Utilities;
using WpfClient.MVVM.Model;
using Services.Dialogs;
using System.Windows.Input;


namespace WpfClient.MVVM.ViewModel
{
    internal class RegisterClientViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private Client _clienteActual = new Client();

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

        public ICommand RegisterClienetCommand { get; set; }

        public ICommand NavigateToSearchClientView { get; set; }
        public RegisterClientViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _dialogService = dialogService;

            Navigation = navigationService;
            NavigateToSearchClientView = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchClientViewModel>();
                },
                o => true);
            RegisterClienetCommand = new RelayCommand(
                o =>
                {
                    var confirmationVM = new ConfirmationViewModel($"¿Deseas registrar al cliente {ClienteActual.Name}?");
                    var result = _dialogService.ShowDialog(confirmationVM);

                    if (result == true)
                    {
                        //TODO: Lógica para guardar ClienteActual en base de datos
                    }
                },
                o => true);
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Client client)
            {
                ClienteActual = client;
            }
        }
    }
}
