using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IAdministrator: IBaseRepository<Administrador>
    {
        Task<Administrador> SearchByIdAsync(int id, AccountStatusEnum statusEnum);
    }
}
