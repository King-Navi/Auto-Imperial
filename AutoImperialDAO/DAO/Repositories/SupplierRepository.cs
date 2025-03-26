using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    public class SupplierRepository : BaseRepository<Proveedor>, ISupplierRepository
    {
        public SupplierRepository(AutoImperialContext context) : base(context)
        {
        }

        public bool Register(Proveedor proveedor)
        {
            throw new NotImplementedException();
        }

        public Task<Proveedor> SearchByCURPAsync(string CURP, AccountStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }

        public Task<List<Proveedor>> SearchByCurpRfcNameAsync(string parameter, AccountStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }

        public Task<Proveedor> SearchByIdAsync(int id, AccountStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }
    }
}
