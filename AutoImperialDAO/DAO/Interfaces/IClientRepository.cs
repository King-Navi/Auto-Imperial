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
        Task<List<Cliente>> SearchByCurpRfcNameAsync(string parameter, AccountStatusEnum statusEnum);
        /// <summary>
        /// Asynchronously searches for clients using pagination and filtering by account status.
        /// </summary>
        /// <param name="startPage">The starting page number from which to begin retrieving clients.</param>
        /// <param name="totalPages">The total number of pages to retrieve.</param>
        /// <param name="status">The account status filter to apply to the search.</param>
        /// <param name="pageSize">The number of clients to retrieve per page (default is 50).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of clients.</returns>
        Task<List<Cliente>> SearchByPagesAsync(int startPage, int totalPages, AccountStatusEnum status, int pageSize = 50);
    }
}
