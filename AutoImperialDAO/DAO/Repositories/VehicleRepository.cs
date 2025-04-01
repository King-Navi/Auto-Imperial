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
    public class VehicleRepository : BaseRepository<Vehiculo>, IVehicleRepository
    {
        public VehicleRepository(AutoImperialContext context) : base(context)
        {
        }

        public async Task<List<Marca>> GetAllBranchAsync()
        {
            List<Marca> result = new();
            try
            {
                result = await _context.Marcas.ToListAsync(); 
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAllAsync: {ex.Message}");
                result = new List<Marca>();
            }

            return result;
        }

        public async Task<List<Modelo>> GetModelsByBrandIdAsync(int idMarca)
        {
            List<Modelo> result = new();
            try
            {
                result = await _context.Modelos
                    .Where(m => m.idMarca == idMarca)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetModelsByBrandIdAsync: {ex.Message}");
                result = new List<Modelo>();
            }

            return result;
        }

        public async Task<List<AutoImperialDAO.Models.Version>> GetVersionsByModelIdAsync(int idModelo)
        {
            List<AutoImperialDAO.Models.Version> result = new();
            try
            {
                result = await _context.Versiones
                    .Where(v => v.idModelo == idModelo)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetVersionsByModelIdAsync: {ex.Message}");
                result = new List<AutoImperialDAO.Models.Version>();
            }
        
            return result;
        }
    }
}
