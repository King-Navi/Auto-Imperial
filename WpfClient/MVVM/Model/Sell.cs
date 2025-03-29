using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WpfClient.MVVM.Model
{
    class Sell : Venta, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public new int SellId
        {
            get => base.idVenta;
            set { base.idVenta = value; OnPropertyChanged(); }
        }

        public new DateOnly SellDate
        {
            get => base.fechaVenta;
            set { base.fechaVenta = value; OnPropertyChanged(); }
        }

        public new decimal? VehiclePrice
        {
            get => base.precioVehiculo;
            set { base.precioVehiculo = value; OnPropertyChanged(); }
        }

        public new string? PaymentMethod
        {
            get => base.formaPago;
            set { base.formaPago = value; OnPropertyChanged(); }
        }

        public new string? AdditionalNotes
        {
            get => base.notasAdicionales;
            set { base.notasAdicionales = value; OnPropertyChanged(); }
        }

        public new int ReservationId
        {
            get => base.idReserva;
            set { base.idReserva = value; OnPropertyChanged(); }
        }

        public new int VehicleId
        {
            get => base.idVehiculo;
            set { base.idVehiculo = value; OnPropertyChanged(); }
        }

        public new Reserva Reservation
        {
            get => base.idReservaNavigation;
            set { base.idReservaNavigation = value; OnPropertyChanged(); }
        }

        public new Vehiculo Vehicle
        {
            get => base.idVehiculoNavigation;
            set { base.idVehiculoNavigation = value; OnPropertyChanged(); }
        }

        public object Clone()
        {
            var clone = (Sell)Activator.CreateInstance(typeof(Sell));

            foreach (PropertyInfo prop in typeof(Sell).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    prop.SetValue(clone, prop.GetValue(this));
                }
            }

            return clone;
        }
    }
}
