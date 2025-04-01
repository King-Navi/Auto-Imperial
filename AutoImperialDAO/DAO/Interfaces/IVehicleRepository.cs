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


    }
}
