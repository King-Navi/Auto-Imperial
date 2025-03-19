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
    class Employee : AutoImperialDAO.Models.Vendedor , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //TODO add more atributes

        public new int IdEmployee
        {
            get => base.idVendedor;
            set { base.idVendedor = value; OnPropertyChanged(); }
        }

        public new string Name
        {
            get => base.nombre;
            set { base.nombre = value; OnPropertyChanged(); }
        }

        public new string PaternalSurname
        {
            get => base.apellidoPaterno;
            set { base.apellidoPaterno = value; OnPropertyChanged(); }
        }

        public new string MaternalSurname
        {
            get => base.apellidoMaterno;
            set { base.apellidoMaterno = value; OnPropertyChanged(); }
        }

        public new string? Phone
        {
            get => base.telefono;
            set { base.telefono = value; OnPropertyChanged(); }
        }

        public new string? Email
        {
            get => base.correo;
            set { base.correo = value; OnPropertyChanged(); }
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

        public new string? CP
        {
            get => base.codigoPostal;
            set { base.codigoPostal = value; OnPropertyChanged(); }
        }

        public new string? City
        {
            get => base.ciudad;
            set { base.ciudad = value; OnPropertyChanged(); }
        }

        public new string? RFC
        {
            get => base.RFC;
            set { base.RFC = value; OnPropertyChanged(); }
        }

        public new string CURP
        {
            get => base.CURP;
            set { base.CURP = value; OnPropertyChanged(); }
        }

        public new string State
        {
            get => base.estadoCuenta;
            set { base.estadoCuenta = value; OnPropertyChanged(); }
        }
        

        public new string PositionVendor
        {
            get => base.puestoVendedor;
            set { base.puestoVendedor = value; OnPropertyChanged(); }
        }

        public new string EmployeeNumber
        {
            get => base.numeroEmpleado;
            set { base.numeroEmpleado = value; OnPropertyChanged(); }
        }

        public new string Branch
        {
            get => base.sucursal;
            set { base.sucursal = value; OnPropertyChanged(); }
        }

        public new string Username
        {
            get => base.nombreUsuario;
            set { base.nombreUsuario = value; OnPropertyChanged(); }
        }

        public new string Password
        {
            get => base.password;
            set { base.password = value; OnPropertyChanged(); }
        }

        public ICollection<Reserva> Reservas;


        public object Clone()
        {
            var clone = (Employee)Activator.CreateInstance(typeof(Employee));

            foreach (PropertyInfo prop in typeof(Employee).GetProperties(BindingFlags.Public | BindingFlags.Instance))
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
