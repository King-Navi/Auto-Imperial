using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.MVVM.Model;

namespace WpfClient.MVVM.ViewModel
{
    internal class ReserveViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private Client _clienteActual = new Client();

        public Client ClienteActual
        {
            get => _clienteActual;
            set { _clienteActual = value; OnPropertyChanged(); }
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

        public ReserveViewModel(INavigationService navigation, IDialogService dialogService)
        {
            Navigation = navigation;
            _dialogService = dialogService;
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
