using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AutoImperialContext context) : base(context)
        {
        }

        public User Authenticate(string username, string password)
        {
            var admin = _context.Administradors
                .FirstOrDefault(a => a.nombreUsuario.ToLower() == username.ToLower()
                                  && a.password == password);
            if (admin != null)
            {
                return new User
                {
                    Username = admin.nombreUsuario,
                    Password = admin.password,
                    Role = "Admin",
                };
            }

            var vendedor = _context.Vendedores
                .FirstOrDefault(v => v.nombreUsuario.ToLower() == username.ToLower()
                                  && v.password == password);
            if (vendedor != null)
            {
                return new User
                {
                    Username = vendedor.nombreUsuario,
                    Password = vendedor.password,
                    Role = "Vendedor",
                };
            }

            return null;
        }
    }
}
