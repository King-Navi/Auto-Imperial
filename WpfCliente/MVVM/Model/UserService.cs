using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCliente.MVVM.Model
{
    public class UserService
    {
        private UserModel _usuarioAutenticado;

        public UserModel UsuarioAutenticado => _usuarioAutenticado;

        public bool IsAuthenticated => _usuarioAutenticado != null;

        public void GuardarUsuario(string nombre, string contraseña)
        {
            _usuarioAutenticado = new UserModel(nombre, contraseña);
        }

        public void CerrarSesion()
        {
            _usuarioAutenticado = null;
        }
    }
}
