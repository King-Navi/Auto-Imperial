using AutoImperialDAO.Enums;
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
        private string _vehicle;
        private ImageSource _vehicleImage = new BitmapImage(new Uri(PathsIcons.DEFAULT_CAR));
        private ReserveStatusEnum _reservationStatus;

        public string Vehicle
        {
            get => _vehicle;
            set { _vehicle = value; OnPropertyChanged(); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReserveCardModel() { }

        public ReserveCardModel(string vehicle, ReserveStatusEnum status, ImageSource image)
        {
            Vehicle = vehicle;
            ReservationStatus = status;
            VehicleImage = image;
        }
    }
}
