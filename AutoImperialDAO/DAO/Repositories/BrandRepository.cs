using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoImperialDAO.DAO.Repositories
{
    public class BrandRepository : BaseRepository<Marca>, IBrandRepository
    {
        public BrandRepository(AutoImperialContext context) : base(context)
        {
        }

        public List<Marca> GetAllBrandsWithModelsAndVersions()
        {
            try
            {
                return  _context.Marca
                        .Include(m => m.Modelo)
                            .ThenInclude(mod => mod.Version)
                        .ToList();
            }
            catch (Exception)
            {
            }

            return new List<Marca>() { new Marca { idMarca = -1 } };
        }

    }
}
