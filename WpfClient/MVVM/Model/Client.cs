using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.MVVM.Model
{
    public class Client : AutoImperialDAO.Models.Cliente , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public new int IdClient
        {
            get => base.idCliente;
            set { base.idCliente = value; OnPropertyChanged(); }
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
            get => base.estado;
            set { base.estado = value; OnPropertyChanged(); }
        }
    }
}
