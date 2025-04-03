using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IVehicleRepository : IBaseRepository<Vehiculo>
    {
        Task<List<Marca>> GetAllBranchAsync();
        Task<List<Modelo>> GetModelsByBrandIdAsync(int idMarca);
        Task<List<AutoImperialDAO.Models.Version>> GetVersionsByModelIdAsync(int idModelo);
        Task<bool> RegisterVehicleAsync(Vehiculo vehiculo);
        Task<List<Vehiculo>> SearchVehicleAsync(string search, VehicleStatus statusEnum);
        Task<bool> EditVehicleAsync(Vehiculo vehiculo);
        bool DeleteById(int id);
        List<Vehiculo> AdvancedSearchVehicle(Utilities.VehicleSearch search, VehicleStatus statusEnum);



    }
}
