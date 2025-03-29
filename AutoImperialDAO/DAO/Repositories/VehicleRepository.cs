using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
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

        
    }
}
