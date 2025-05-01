using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Repositories
{
    public class PhotoRepository : BaseRepository<Foto> , IPhotoRepository
    {
        public PhotoRepository(AutoImperialContext context) : base(context)
        {
        }

        public byte[] GetPhotoByIdVehicle(int idVersion)
        {
            try
            {
                var photo = _context.Fotos
                    .Where(f => f.idVehiculoNavigation.idVersion == idVersion)
                    .Select(f => f.foto)
                    .FirstOrDefault();

                return photo;
            }
            catch (Exception)
            {
                // Log if needed
                return null;
            }
        }
    }
}
