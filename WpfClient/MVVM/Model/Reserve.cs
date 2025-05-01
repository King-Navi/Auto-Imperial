using AutoImperialDAO.Models;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WpfClient.MVVM.Model
{
    public class Reserve : Reserva, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Reserve() { }

        public Reserve(Reserva dbReserve)
        {
            IdReserve = dbReserve.idReserva;
            ReserveDate = dbReserve.fechaReserva;
            DownPayment = dbReserve.montoEnganche;
            AdditionalNotes = dbReserve.notasAdicionales;
            IdSeller = dbReserve.idVendedor;
            IdClient = dbReserve.idCliente;
            IdVersion = dbReserve.idVersion;

            if (dbReserve.idClienteNavigation != null)
                Client = new Client(dbReserve.idClienteNavigation);

            if (dbReserve.idVendedorNavigation != null)
                Seller = new SellerEmployee(dbReserve.idVendedorNavigation);

            if (dbReserve.idVersionNavigation != null)
                Version = new VersionModel(dbReserve.idVersionNavigation);
        }

        public new int IdReserve
        {
            get => base.idReserva;
            set { base.idReserva = value; OnPropertyChanged(); }
        }

        public new DateTime ReserveDate
        {
            get => base.fechaReserva;
            set { base.fechaReserva = value; OnPropertyChanged(); }
        }

        public new decimal? DownPayment
        {
            get => base.montoEnganche;
            set { base.montoEnganche = value; OnPropertyChanged(); }
        } 

        public new string? AdditionalNotes
        {
            get => base.notasAdicionales;
            set { base.notasAdicionales = value; OnPropertyChanged(); }
        }

        public new int IdSeller
        {
            get => base.idVendedor;
            set { base.idVendedor = value; OnPropertyChanged(); }
        }

        public new int IdClient
        {
            get => base.idCliente;
            set { base.idCliente = value; OnPropertyChanged(); }
        }

        public new int IdVersion
        {
            get => base.idVersion;
            set { base.idVersion = value; OnPropertyChanged(); }
        }

        public Client? Client { get; set; }

        public SellerEmployee? Seller { get; set; }

        public VersionModel? Version { get; set; }

        public object Clone()
        {
            var clone = (Reserve)Activator.CreateInstance(typeof(Reserve));
            foreach (var prop in typeof(Reserve).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(clone, prop.GetValue(this));
            }
            return clone;
        }
    }
}
