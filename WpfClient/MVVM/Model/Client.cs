using AutoImperialDAO.Models;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WpfClient.MVVM.Model
{
    public class Client : AutoImperialDAO.Models.Cliente , INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Client()
        {
        }

        public Client(Cliente dataBaseModelClient)
        {
            IdClient = dataBaseModelClient.idCliente;
            Name = dataBaseModelClient.nombre;
            PaternalSurname = dataBaseModelClient.apellidoPaterno;
            MaternalSurname = dataBaseModelClient.apellidoMaterno;
            Phone = dataBaseModelClient.telefono;
            Email = dataBaseModelClient.correo;
            Street = dataBaseModelClient.calle;
            Number = dataBaseModelClient.numero;
            CP = dataBaseModelClient.codigoPostal;
            City = dataBaseModelClient.ciudad;
            RFC = dataBaseModelClient.RFC;
            CURP = dataBaseModelClient.CURP;
            State = dataBaseModelClient.estado;
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

        public override string? ToString()
        {
            return Name + " " + PaternalSurname + " " + MaternalSurname;
        }



        /// <summary>
        /// Implementation of the Prototype pattern to clone the object.
        /// </summary>
        /// <returns>A new Client instance with the same values.</returns>
        public object Clone()
        {
            var clone = (Client)Activator.CreateInstance(typeof(Client));

            foreach (PropertyInfo prop in typeof(Client).GetProperties(BindingFlags.Public | BindingFlags.Instance))
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
