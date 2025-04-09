using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Version = AutoImperialDAO.Models.Version;

namespace AutoImperialDAO.DAO.Repositories
{
    public class VersionRepository : BaseRepository<Version>, IVersionRepository
    {
        public VersionRepository(AutoImperialContext context) : base(context)
        {
        }
        public List<Version> GetAllVersionsWithModelAndBrand()
        {
            try
            {
                return _context.Version
                    .Include(v => v.idModeloNavigation)
                        .ThenInclude(m => m.idMarcaNavigation)
                    .ToList();
            }
            catch (Exception)
            {
            }

            return new List<Version> { new Version { idVersion = -1 } };
        }

        public string GetFullVehicleName(int idVersion)
        {
            try
            {
                var nombreCompleto = _context.Version
                    .Where(v => v.idVersion == idVersion)
                    .Select(v =>
                        v.idModeloNavigation.idMarcaNavigation.nombre + " " +
                        v.idModeloNavigation.nombre + " " +
                        v.nombre + " " + v.motor)
                    .FirstOrDefault();

                return nombreCompleto ?? "Vehículo no encontrado";
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Version? GetVersionById(int id)
        {
            try
            {
                return _context.Version.FirstOrDefault(v => v.idVersion == id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
