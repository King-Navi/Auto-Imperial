using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    internal class SearchClientViewModel : Services.Navigation.ViewModel
    {
        private const int FIRST_SEARCH_INIT = 1;
        private const int FIRST_SEARCH_PAGE_SIZE = 5;
        private const int PAGE_SIZE = 100;
        private const int SECONDS_ERROR_MSG = 6;
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
        private IClientRepository _clientRepository;
        private ObservableCollection<ClientCardViewModel> clientsList = new ObservableCollection<ClientCardViewModel>();
        public ObservableCollection<ClientCardViewModel> ClientsList { get => clientsList; set => clientsList = value; }
        private ClientCardViewModel? _selected;
        public ClientCardViewModel? Selected
        {
            get => _selected;
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged(nameof(Selected));
                }
            }
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
        private Visibility _errorMessageVisibility = Visibility.Collapsed;
        public Visibility ErrorMessageVisibility
        {
            get => _errorMessageVisibility;
            set
            {
                _errorMessageVisibility = value;
                OnPropertyChanged(nameof(ErrorMessageVisibility));
            }
        }

        public ICommand NavegateToRegisterClientViewCommand { get; set; }
        public ICommand DeleteClientCommand { get; set; }
        public ICommand EditClientCommand { get; set; }
        public IRelayCommand SearchCommand { get; set; }
        private IDialogService _dialogService;

        public SearchClientViewModel(INavigationService navigationService, IClientRepository clientRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _clientRepository = clientRepository;
            _ = InitializeAsync();
            Navigation = navigationService;
            NavegateToRegisterClientViewCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<RegisterClientViewModel>();
                },
                o => true);
            DeleteClientCommand = new RelayCommand(
                o =>
                {
                    var confirmationVM = new ConfirmationViewModel("Confimracion de registro", $"¿Deseas registrar al cliente?", Utilities.Enum.ConfirmationIconType.WarningIcon);
                    var result = _dialogService.ShowDialog(confirmationVM);
                    if (false == result)
                    {
                        return;
                    }
                    if (Selected != null)
                    {
                        _clientRepository.DeleteById(Selected.ClientActual.IdClient);
                        ClientsList.Remove(Selected);
                        Selected = null;
                    }
                },
                o => Selected != null);
            EditClientCommand = new RelayCommand(
                o =>
                {
                    if (Selected != null)
                    {
                        Navigation.NavigateTo<RegisterClientViewModel>(Selected.ClientActual);
                        Selected = null;
                    }
                },
                o => Selected != null);
            SearchCommand = new RelayCommand(
                async o =>
                {
                    if (!String.IsNullOrWhiteSpace(SearchText))
                    {
                        var clientes = await SearchClientCurpRfcNameAsync();
                        FillList(ConvertToClientCardViewModel(clientes));
                        Selected = null;
                    }
                },
                o => !String.IsNullOrWhiteSpace(SearchText));
        }
        private async Task<List<Cliente>> SearchClientCurpRfcNameAsync()
        {
            Selected = null;
            try
            {
                if (!String.IsNullOrWhiteSpace(SearchText))
                {
                    var result = await _clientRepository.SearchByCurpRfcNameAsync(
                        SearchText, AutoImperialDAO.Enums.AccountStatusEnum.Activo);
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new List<Cliente>();
        }
        private async Task InitializeAsync()
        {
            try
            {
                var resultado = await SearchClientsAsync(FIRST_SEARCH_INIT, FIRST_SEARCH_PAGE_SIZE, AutoImperialDAO.Enums.AccountStatusEnum.Activo);
                FillList(ConvertToClientCardViewModel(resultado));
            }
            catch (Exception)
            {
            }
        }

        public async Task<List<Cliente>> SearchClientsAsync(int startPage, int totalPage, AutoImperialDAO.Enums.AccountStatusEnum status)
        {
            try
            {
                return await _clientRepository.SearchByPagesAsync(startPage, totalPage, status, PAGE_SIZE);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private ObservableCollection<ClientCardViewModel> ConvertToClientCardViewModel(List<Cliente> list)
        {
            ClientsList.Clear();
            foreach (var clientedbModel in list)
            {
                ClientsList.Add(new ClientCardViewModel(Navigation, new Client(clientedbModel)));
            }

            return ClientsList;
        }
        public void FillList(ObservableCollection<ClientCardViewModel> clientCardViews)
        {
            if (clientCardViews.Count == 1 && clientCardViews[0].ClientActual.IdClient == -1)
            {
                ShowErrorMessage();
                return;
            }
            ClientsList = clientCardViews;
        }
        private async void ShowErrorMessage()
        {
            ErrorMessageVisibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(SECONDS_ERROR_MSG)); 
            ErrorMessageVisibility = Visibility.Collapsed;
        }
    }
}
