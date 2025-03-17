using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    internal class ReserveViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private Client _currentClient = new Client();

        public Client CurrentClient
        {
            get => _currentClient;
            set { _currentClient = value; OnPropertyChanged(); }
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

        public ICommand NavigateToSearchCommand { get; set; }
        public ICommand RegisterReserveCommand { get; set; }

        public ReserveViewModel(INavigationService navigation, IDialogService dialogService)
        {
            Navigation = navigation;
            _dialogService = dialogService;
            NavigateToSearchCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchClientViewModel>();
                },
                o => true);
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Client client)
            {
                CurrentClient = client;
            }
        }
    }
}
