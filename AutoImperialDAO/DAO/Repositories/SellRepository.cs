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
        bool ISellRepository.DeleteById(int id)
        {
            //TODO
            throw new NotImplementedException();
        }

        bool ISellRepository.Edit(Cliente client)
        {
            //TODO
            throw new NotImplementedException();
        }

        bool ISellRepository.Register(Cliente client)
        {
            //TODO
            throw new NotImplementedException();
        }

        Task<Venta> ISellRepository.SearchByIdAsync(int id)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
