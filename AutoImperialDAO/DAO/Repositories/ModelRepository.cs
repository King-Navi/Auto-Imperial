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
    public class ModelRepository : BaseRepository<Modelo>, IModeloRepository
    {
        public ModelRepository(AutoImperialContext context) : base(context)
        {
        }

        public List<Modelo> GetAllModelsWithBrandAndVersions()
        {
            try
            {
                return _context.Modelo
                    .Include(m => m.idMarcaNavigation)
                    .Include(m => m.Version)
                    .ToList();
            }
            catch (Exception)
            {
            }

            return new List<Modelo> { new Modelo { idModelo = -1 } };
        }

    }
}
