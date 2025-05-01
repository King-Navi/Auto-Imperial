using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IBrandRepository : IBaseRepository<Marca>
    {
        List<Marca> GetAllBrandsWithModelsAndVersions();
    }
}
