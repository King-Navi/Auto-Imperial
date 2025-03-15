using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IClientRepository : IBaseRepository<Cliente>
    {
        bool Register(Cliente client);
        bool DeleteById(int id);
        bool EditById(Cliente client);
        Cliente SearchById(int id);
        List<Cliente> SearchById(List<int> ids, int page, int pageSize = 50);
    }
}
