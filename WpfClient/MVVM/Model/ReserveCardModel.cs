using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfClient.Utilities;

namespace WpfClient.MVVM.Model
{
    public class ReserveCardModel : INotifyPropertyChanged
    {
        private string _vehicleName;
        private ImageSource _vehicleImage = new BitmapImage(new Uri(PathsIcons.DEFAULT_CAR));
        private ReserveStatusEnum _reservationStatus;
        private Sell? _Sell = null;
        private  VersionModel _version;
        private Client _client;
        private Reserve _reserve;
        public required string VehicleName
        {
            get => _vehicleName;
            set { _vehicleName = value; OnPropertyChanged(); }
        }

        public ImageSource VehicleImage
        {
            get => _vehicleImage;
            set { _vehicleImage = value; OnPropertyChanged(); }
        }

        public ReserveStatusEnum ReservationStatus
        {
            get => _reservationStatus;
            set { _reservationStatus = value; OnPropertyChanged(); }
        }

        public required Reserve Reserve
        {
            get => _reserve;
            set { _reserve = value; OnPropertyChanged(); }
        }

        public required Client Client
        {
            get => _client;
            set { _client = value; OnPropertyChanged(); }
        }

        public required VersionModel Version
        {
            get => _version;
            set { _version = value; OnPropertyChanged(); }
        }

        public Sell? Sell
        {
            get => _Sell;
            set { _Sell = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
