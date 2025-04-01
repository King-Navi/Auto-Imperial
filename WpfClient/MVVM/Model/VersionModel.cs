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
    public class VersionModel : AutoImperialDAO.Models.Version, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public VersionModel() { }

        public VersionModel(AutoImperialDAO.Models.Version dbVersion)
        {
            IdVersion = dbVersion.idVersion;
            Name = dbVersion.nombre;
            Transmission = dbVersion.transmision;
            Doors = dbVersion.puertas;
            Engine = dbVersion.motor;
            IdModel = dbVersion.idModelo;
        }

        public new int IdVersion
        {
            get => base.idVersion;
            set { base.idVersion = value; OnPropertyChanged(); }
        }

        public new string Name
        {
            get => base.nombre;
            set { base.nombre = value; OnPropertyChanged(); }
        }

        public new string? Transmission
        {
            get => base.transmision;
            set { base.transmision = value; OnPropertyChanged(); }
        }

        public new int? Doors
        {
            get => base.puertas;
            set { base.puertas = value; OnPropertyChanged(); }
        }

        public new string? Engine
        {
            get => base.motor;
            set { base.motor = value; OnPropertyChanged(); }
        }

        public new int IdModel
        {
            get => base.idModelo;
            set { base.idModelo = value; OnPropertyChanged(); }
        }

        public object Clone()
        {
            var clone = (VersionModel)Activator.CreateInstance(typeof(VersionModel));
            foreach (var prop in typeof(VersionModel).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(clone, prop.GetValue(this));
            }
            return clone;
        }
    }

}
