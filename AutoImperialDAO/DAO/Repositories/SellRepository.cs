using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    public class SellRepository : BaseRepository<Venta>, ISellRepository
    {
        public SellRepository(AutoImperialContext context) : base(context)
        {
        }
        public bool DeleteById(int id)
        {
            //TODO
            throw new NotImplementedException();
        }

        public bool Edit(Cliente client)
        {
            //TODO
            throw new NotImplementedException();
        }

        public bool Register(Cliente client)
        {
            //TODO
            throw new NotImplementedException();
        }

        public Task<Venta> SearchByIdAsync(int id)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
