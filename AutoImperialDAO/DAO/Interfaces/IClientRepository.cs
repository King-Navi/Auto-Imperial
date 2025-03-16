using AutoImperialDAO.Enums;
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
        bool Edit(Cliente client);
        Task<Cliente> SearchByIdAsync(int id, AccountStatusEnum statusEnum);
        Task<Cliente> SearchByCURPAsync(string CURP, AccountStatusEnum statusEnum);
        Task<List<Cliente>> SearchByPagesAsync(int startPage, int totalPages, AccountStatusEnum status, int pageSize = 50);
    }
}
