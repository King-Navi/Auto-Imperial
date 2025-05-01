using AutoImperialDAO.Models;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WpfClient.MVVM.Model
{
    class SupplierPayment : AutoImperialDAO.Models.CompraProveedor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public new int SupplierPaymentId
        {
            get => base.idCompraProveedor;
            set { base.idCompraProveedor = value; OnPropertyChanged(); }
        }

        public new decimal? TotalAmount
        {
            get => base.montoTotal;
            set { base.montoTotal = value; OnPropertyChanged(); }
        }

        public new string Folio
        {
            get => base.folio;
            set { base.folio = value; OnPropertyChanged(); }
        }

        public new DateOnly PurchaseDate
        {
            get => base.fechaCompra;
            set { base.fechaCompra = value; OnPropertyChanged(); }
        }

        public new int AdministratorId
        {
            get => base.idAdministrador;
            set { base.idAdministrador = value; OnPropertyChanged(); }
        }

        public new int SupplierId
        {
            get => base.idProveedor;
            set { base.idProveedor = value; OnPropertyChanged(); }
        }

        public new int VehiclesCount
        {
            get => base.vehiculosComprados;
            set { base.vehiculosComprados = value; OnPropertyChanged(); }
        }


        public new ICollection<Vehiculo> Vehiculos
        {
            get => base.Vehiculo;
            set { base.Vehiculo = value; OnPropertyChanged(); }
        }

        public object Clone()
        {
            var clone = (SupplierPayment)Activator.CreateInstance(typeof(SupplierPayment));
            foreach (PropertyInfo prop in typeof(SupplierPayment).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(clone, prop.GetValue(this));
            }
            return clone;
        }
    }
}
