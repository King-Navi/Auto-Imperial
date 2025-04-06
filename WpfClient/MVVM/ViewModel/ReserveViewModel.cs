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
        private readonly ISellRepository _sell;
        public ICommand NavigateToSearchCommand { get; set; }
        public ICommand RegisterReserveCommand { get; set; }

        public ReserveViewModel(INavigationService navigation, IDialogService dialogService , IEmployeeRepository employeeRepository,
            IBrandRepository brand, IReserveRepository reserve, IVersionRepository version, IPhotoRepository photo, UserService employee, 
            ISellRepository sell)
        {
            _sell = sell;
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
            RegisterReserveCommand = new RelayCommand(GoRegisterAsync);
            UpdateCollection();
        }



        public async void GoRegisterAsync()
        {
            var window = new RegisterReserveViewModel(await RecoverEmployee(user.CurrentUser.Id), _brand, _reserve , _dialogService , this);
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
                return new List<ReserveCardModel>() { };
            }
            List<ReserveCardModel> reserveCardModels = new();
            foreach (var reserve in reserves)
            {
                try
                {
                    var imageDefault = new BitmapImage(new Uri(PathsIcons.DEFAULT_CAR));
                    var vehicleImage = ImageManager.ByteArrayToImageSource(_photo.GetPhotoByIdVehicle(reserve.idVersion)) ?? imageDefault;

                    var vehicleName = _version.GetFullVehicleName(reserve.idVersion);
                    if (string.IsNullOrEmpty(vehicleName))
                        throw new ArgumentNullException(nameof(vehicleName), $"Nombre del vehículo con ID {reserve.idVersion} es null o vacío.");

                    var reservaData = _reserve.GetReserveById(reserve.idReserva);
                    if (reservaData == null)
                        throw new ArgumentNullException(nameof(reservaData), $"Reserva con ID {reserve.idReserva} es null.");

                    var versionData = _version.GetVersionById(reserve.idVersion);
                    if (versionData == null)
                        throw new ArgumentNullException(nameof(versionData), $"Versión con ID {reserve.idVersion} es null.");

                    Sell? sellModel = null;
                    var ventaData = _sell.GetSellByIdReserve(reserve.idReserva);
                    if (ventaData != null)
                    {
                        sellModel = new Sell(ventaData);
                    }

                    var reserveCardModel = new ReserveCardModel
                    {
                        VehicleName = vehicleName,
                        VehicleImage = vehicleImage,
                        ReservationStatus = (ReserveStatusEnum)Enum.Parse(typeof(ReserveStatusEnum), reserve.estado),
                        Reserve = new Reserve(reservaData),
                        Client = CurrentClient,
                        Version = new VersionModel(versionData),
                        Sell = sellModel
                    };

                    reserveCardModels.Add(reserveCardModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la reserva con ID {reserve.idReserva}: {ex.Message}");
                    throw; // O continuás sin lanzar si no querés que reviente
                }
            }
            return reserveCardModels;
        }

        public void UpdateCollection()
        {
            ReserveCards.Clear();
            List<Reserva> interested = RecoverReservesSeller(user.CurrentUser.Id, AutoImperialDAO.Enums.ReserveStatusEnum.Interesado); 
            List<Reserva> booked = RecoverReservesSeller(user.CurrentUser.Id, AutoImperialDAO.Enums.ReserveStatusEnum.Apartado); 
            List<Reserva> selled = RecoverReservesSeller(user.CurrentUser.Id, AutoImperialDAO.Enums.ReserveStatusEnum.Vendido); 
            
            var listInterested = ConvertToReserveCardModel(interested);
            var listBooked = ConvertToReserveCardModel(booked);
            var listSelled = ConvertToReserveCardModel(selled);

            foreach (var reserve in listInterested)
            {
                ReserveCards.Add(new ReserveCardViewModel(navigation, reserve));
            }
            foreach (var reserve in listBooked)
            {
                ReserveCards.Add(new ReserveCardViewModel(navigation, reserve));
            }

            foreach (var reserve in listSelled)
            {
                ReserveCards.Add(new ReserveCardViewModel(navigation, reserve));
            }
        }
    }
}
