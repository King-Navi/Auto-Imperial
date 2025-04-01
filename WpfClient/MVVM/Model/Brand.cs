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
    public class Brand : AutoImperialDAO.Models.Marca, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Brand() { }

        public Brand(Marca dbBrand)
        {
            IdBrand = dbBrand.idMarca;
            Name = dbBrand.nombre;
            Models = ConvertToModelBase(dbBrand.Modelo.ToList());

        }

        public new int IdBrand
        {
            get => base.idMarca;
            set { base.idMarca = value; OnPropertyChanged(); }
        }

        public new string Name
        {
            get => base.nombre;
            set { base.nombre = value; OnPropertyChanged(); }
        }
        public List<ModelBase> Models { get; set; } = new();

        public List<ModelBase> ConvertToModelBase( List<AutoImperialDAO.Models.Modelo> models)
        {
            var result = new List<ModelBase>();

            try
            {
                foreach (var actualModel in models)
                {
                    result.Add(new ModelBase(actualModel));
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public object Clone()
        {
            var clone = (Brand)Activator.CreateInstance(typeof(Brand));
            foreach (var prop in typeof(Brand).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(clone, prop.GetValue(this));
            }
            return clone;
        }
    }

}
