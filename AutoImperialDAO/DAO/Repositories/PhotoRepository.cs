using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
