using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
namespace AutoImperialDAO.DAO.Repositories
{
    public partial class ClientRepository : BaseRepository<Cliente>
    {
        public ClientRepository(DbContext context) : base(context)
        {
        }
    }
}
