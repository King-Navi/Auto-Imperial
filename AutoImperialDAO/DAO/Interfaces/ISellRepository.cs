using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface ISellRepository
    {
        bool Register(Cliente client);
        bool DeleteById(int id);
        bool Edit(Cliente client);
        Task<Ventum> SearchByIdAsync(int id);
    }
}
