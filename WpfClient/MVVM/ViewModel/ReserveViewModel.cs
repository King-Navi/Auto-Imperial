using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
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
using System.Windows.Media.Imaging;
using WpfClient.MVVM.Model;
using WpfClient.Resources.ViewCards;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    public class ReserveViewModel : Services.Navigation.ViewModel, IParameterReceiver, ICollectionUpdater
    {
        private UserService user;
        public ObservableCollection<ReserveCardViewModel> ReserveCards { get; set; } = new();
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
        private readonly IVersionRepository _version;
        private readonly IPhotoRepository _photo;
        public ICommand NavigateToSearchCommand { get; set; }
        public ICommand RegisterReserveCommand { get; set; }

        public ReserveViewModel(INavigationService navigation, IDialogService dialogService , IEmployeeRepository employeeRepository, 
            IBrandRepository brand, IReserveRepository reserve , IVersionRepository version, IPhotoRepository photo, UserService employee)
        {
            _photo = photo;
            _version = version;
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

        public List<Reserva> RecoverReservesSeller(int idSeller, AutoImperialDAO.Enums.ReserveStatusEnum statusEnum)
        {
            List<Reserva> consulta = _reserve.GetReservesByIdSeller(idSeller, statusEnum);
            if (consulta.Count == 1 && consulta.FirstOrDefault()?.idReserva == -1)
            {
                return null;
            }
            return consulta;
        }
        public List<ReserveCardModel> ConvertToReserveCardModel(List<Reserva> reserves)
        {
            if (reserves == null)
            {
                return null;
            }
            List<ReserveCardModel> reserveCardModels = new();
            foreach (var reserve in reserves)
            {
                var imageDefault = new BitmapImage(new Uri(PathsIcons.DEFAULT_CAR));
                var reserveCardModel = new ReserveCardModel(
                    id: reserve.idReserva,
                    vehicle: _version.GetNombreCompletoVehiculo(reserve.idVersion),
                    status: (ReserveStatusEnum)Enum.Parse(typeof(ReserveStatusEnum), reserve.estado),
                    image: ImageManager.ByteArrayToImageSource(_photo.GetPhotoByIdVehicle(reserve.idVersion)) ?? imageDefault
                );
                reserveCardModels.Add(reserveCardModel);
            }
            return reserveCardModels;
        }

        public void UpdateCollection()
        {
            //var interested = RecoverReservesSeller(user.CurrentUser.Id , AutoImperialDAO.Enums.ReserveStatusEnum.Interesado);
            //var booked = RecoverReservesSeller(user.CurrentUser.Id , AutoImperialDAO.Enums.ReserveStatusEnum.Apartado);
            //var selled = RecoverReservesSeller(user.CurrentUser.Id , AutoImperialDAO.Enums.ReserveStatusEnum.Vendido);
            List<Reserva> interested = RecoverReservesSeller(1 , AutoImperialDAO.Enums.ReserveStatusEnum.Interesado); //FIXME: Erase me when the above line is uncommented
            List<Reserva> booked = RecoverReservesSeller(1 , AutoImperialDAO.Enums.ReserveStatusEnum.Apartado); //FIXME: Erase me when the above line is uncommented
            List<Reserva> selled = RecoverReservesSeller(1 , AutoImperialDAO.Enums.ReserveStatusEnum.Vendido); //FIXME: Erase me when the above line is uncommented
            
            var listInterested = ConvertToReserveCardModel(interested);
            var listBooked = ConvertToReserveCardModel(booked);
            var listSelled = ConvertToReserveCardModel(selled);

            foreach (var reserve in listInterested)
            {
                ReserveCards.Add(new ReserveCardViewModel(reserve));
            }
            foreach (var reserve in listBooked)
            {
                ReserveCards.Add(new ReserveCardViewModel(reserve));
            }

            foreach (var reserve in listSelled)
            {
                ReserveCards.Add(new ReserveCardViewModel(reserve));
            }
        }
    }
}
