using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Interfaces
{
    interface IEmployeeRepository : IBaseRepository<Vendedor>
    {
        bool Register(Vendedor vendedor);
        bool DeleteById(int id);
        bool Edit(Vendedor client);
        Task<Vendedor> SearchByIdAsync(int id, AccountStatusEnum statusEnum);
        Task<Vendedor> SearchByCURPAsync(string CURP, AccountStatusEnum statusEnum);
        Task<List<Vendedor>> SearchByCurpRfcNameAsync(string parameter, AccountStatusEnum statusEnum);
        /// <summary>
        /// Asynchronously searches for clients using pagination and filtering by account status.
        /// </summary>
        /// <param name="startPage">The starting page number from which to begin retrieving clients.</param>
        /// <param name="totalPages">The total number of pages to retrieve.</param>
        /// <param name="status">The account status filter to apply to the search.</param>
        /// <param name="pageSize">The number of clients to retrieve per page (default is 50).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of clients.</returns>
        Task<List<Vendedor>> SearchByPagesAsync(int startPage, int totalPages, AccountStatusEnum status, int pageSize = 50);
    }
}
