using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCliente.MVVM.Model
{
    public class UserModel
    {
        public string Nombre { get; set; }
        public string Contraseña { get; set; }

        public UserModel(string nombre, string contraseña)
        {
            Nombre = nombre;
            Contraseña = contraseña;
        }
    }
}
