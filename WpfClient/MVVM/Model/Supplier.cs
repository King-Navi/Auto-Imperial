using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.MVVM.Model
{
   public  class Supplier : AutoImperialDAO.Models.Proveedor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public new int SupplierId
        {
            get => base.idProveedor;
            set { base.idProveedor = value; OnPropertyChanged(); }
        }

        public new string SupplierName
        {
            get => base.nombreProveedor;
            set { base.nombreProveedor = value; OnPropertyChanged(); }
        }

        public new string? Street
        {
            get => base.calle;
            set { base.calle = value; OnPropertyChanged(); }
        }

        public new int? Number
        {
            get => base.numero;
            set { base.numero = value; OnPropertyChanged(); }
        }

        public new string? ZipCode
        {
            get => base.codigoPostal;
            set { base.codigoPostal = value; OnPropertyChanged(); }
        }

        public new string? Email
        {
            get => base.correo;
            set { base.correo = value; OnPropertyChanged(); }
        }

        public new string? Phone
        {
            get => base.telefono;
            set { base.telefono = value; OnPropertyChanged(); }
        }

        public new string? PrimaryContact
        {
            get => base.contactoPrincipal;
            set { base.contactoPrincipal = value; OnPropertyChanged(); }
        }

        public new string? State
        {
            get => base.estado;
            set { base.estado = value; OnPropertyChanged(); }
        }

        public new string? City
        {
            get => base.ciudad;
            set { base.ciudad = value; OnPropertyChanged(); }
        }

        public new ICollection<CompraProveedor> ComprasProveedor
        {
            get => base.CompraProveedor;
            set { base.CompraProveedor = value; OnPropertyChanged(); }
        }

        public object Clone()
        {
            var clone = (Supplier)Activator.CreateInstance(typeof(Supplier));

            foreach (PropertyInfo prop in typeof(Supplier).GetProperties(BindingFlags.Public | BindingFlags.Instance))
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
