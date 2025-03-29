using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    internal class ReserveViewModel : Services.Navigation.ViewModel, IParameterReceiver, ICollectionUpdater
    {
        private UserService user;
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
        private readonly IEmployeeRepository _employee;
        private readonly IBrandRepository _brand;
        private readonly IReserveRepository _reserve;

        public ICommand NavigateToSearchCommand { get; set; }
        public ICommand RegisterReserveCommand { get; set; }

        public ReserveViewModel(INavigationService navigation, IDialogService dialogService , IEmployeeRepository employeeRepository, IBrandRepository brand, IReserveRepository reserve, UserService employee)
        {
            _reserve = reserve;
            _brand = brand;
            user = employee;
            _employee = employeeRepository;
            Navigation = navigation;
            _dialogService = dialogService;
            NavigateToSearchCommand = new RelayCommand(
                o =>
                {
                    Navigation.NavigateTo<SearchClientViewModel>();
                },
                o => true);
            RegisterReserveCommand = new RelayCommand( GoRegisterAsync);
            UpdateCollection();
        }



        public async void GoRegisterAsync()
        {
            //var window = new RegisterReserveViewModel(await RecoverEmployee(user.CurrentUser.Id), _brand, _reserve , _dialogService, this);  
            var window = new RegisterReserveViewModel(await RecoverEmployee(1), _brand, _reserve , _dialogService , this);  //FIXME: Erase me when the above line is uncommented
            window.ReceiveParameter(CurrentClient);
            _dialogService.ShowDialog(window);
        }

        public async Task<SellerEmployee> RecoverEmployee(int idEmployee)
        {
            Vendedor employee = await _employee.SearchByIdAsync(idEmployee, AutoImperialDAO.Enums.AccountStatusEnum.Activo);
            if (employee != null)
            {
                return new SellerEmployee(employee);
            }
            return null;
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is Client client)
            {
                CurrentClient = client;
            }
        }

        public void UpdateCollection()
        {
            //TODO: Show cards
            //var consulta = _reserve.GetReservesByIdSeller(user.CurrentUser.Id , AutoImperialDAO.Enums.ReserveStatusEnum.Interesado);
            var consulta = _reserve.GetReservesByIdSeller(1 , AutoImperialDAO.Enums.ReserveStatusEnum.Interesado); //FIXME: Erase me when the above line is uncommented
            CurrentClient.nombre = string.Empty;
        }
    }
}
