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
    public class ModelBase : AutoImperialDAO.Models.Modelo, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ModelBase() { }

        public ModelBase(Modelo dbModel)
        {
            IdModel = dbModel.idModelo;
            Name = dbModel.nombre;
            IdBrand = dbModel.idMarca;
            Versions = ConvertToVersionModel(dbModel.Version.ToList());
        }

        public new int IdModel
        {
            get => base.idModelo;
            set { base.idModelo = value; OnPropertyChanged(); }
        }

        public new string Name
        {
            get => base.nombre;
            set { base.nombre = value; OnPropertyChanged(); }
        }

        public new int IdBrand
        {
            get => base.idMarca;
            set { base.idMarca = value; OnPropertyChanged(); }
        }
        public List<VersionModel> Versions { get; set; } = new();

        private List<VersionModel> ConvertToVersionModel(List<AutoImperialDAO.Models.Version> versions)
        {
            try
            {
                return versions.Select(v => new VersionModel(v)).ToList();
            }
            catch (Exception)
            {
            }

            return new List<VersionModel>();
        }
        public object Clone()
        {
            var clone = (ModelBase)Activator.CreateInstance(typeof(ModelBase));
            foreach (var prop in typeof(ModelBase).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(clone, prop.GetValue(this));
            }
            return clone;
        }
    }
}
