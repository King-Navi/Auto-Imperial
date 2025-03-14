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
        public new int idCliente
        {
            get => base.idCliente;
            set { base.idCliente = value; OnPropertyChanged(); }
        }

        public new string nombre
        {
            get => base.nombre;
            set { base.nombre = value; OnPropertyChanged(); }
        }

        public new string apellidoPaterno
        {
            get => base.apellidoPaterno;
            set { base.apellidoPaterno = value; OnPropertyChanged(); }
        }

        public new string apellidoMaterno
        {
            get => base.apellidoMaterno;
            set { base.apellidoMaterno = value; OnPropertyChanged(); }
        }

        public new string? telefono
        {
            get => base.telefono;
            set { base.telefono = value; OnPropertyChanged(); }
        }

        public new string? correo
        {
            get => base.correo;
            set { base.correo = value; OnPropertyChanged(); }
        }

        public new string? calle
        {
            get => base.calle;
            set { base.calle = value; OnPropertyChanged(); }
        }

        public new int? numero
        {
            get => base.numero;
            set { base.numero = value; OnPropertyChanged(); }
        }

        public new string? codigoPostal
        {
            get => base.codigoPostal;
            set { base.codigoPostal = value; OnPropertyChanged(); }
        }

        public new string? ciudad
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

        public new string estado
        {
            get => base.estado;
            set { base.estado = value; OnPropertyChanged(); }
        }
    }
}
