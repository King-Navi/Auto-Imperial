using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using AutoImperialDAO.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    public class AdministratorRepository : BaseRepository<Administrador> , IAdministrator
    {
        public AdministratorRepository(AutoImperialContext context) : base(context)
        {
        }
        public List<Administrador> GetAllAdministrators()
        {
            try
            {
                return _context.Administrador.ToList();
            }
            catch (Exception)
            {
                return new List<Administrador>() { new Administrador { idAdministrador = -1 } };
            }
        }
        public async Task<Administrador> SearchByIdAsync(int id, AccountStatusEnum statusEnum)
        {
            Administrador? result = new Administrador();
            try
            {
                Validator.IsIdValid(id);
                result = await _context.Administrador.FirstOrDefaultAsync(c => c.idAdministrador == id && c.estadoCuenta == statusEnum.ToString());

                if (result == null)
                {
                    throw new KeyNotFoundException($"No se encontró un empleado con ID {id}");
                }

                return result;
            }
            catch (Exception)
            {
                result = new Administrador { idAdministrador = -1 };
            }
            return result;
        }
    }
}
