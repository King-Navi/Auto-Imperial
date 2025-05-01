using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface ISupplierRepository : IBaseRepository<Proveedor>
    {
        bool Register(Proveedor proveedor);
        Task<Proveedor> SearchByIdAsync(int id, AccountStatusEnum statusEnum);
        Task<List<Proveedor>> SearchByNameCityAsync(string parameter, AccountStatusEnum statusEnum);
    }
}
