using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface ISupplierRepository : IBaseRepository<Proveedor>
    {
        bool Register(Proveedor proveedor);
        Task<Proveedor> SearchByIdAsync(int id, AccountStatusEnum statusEnum);
        Task<List<Proveedor>> SearchByNameCityAsync(string parameter, AccountStatusEnum statusEnum);
    }
}
