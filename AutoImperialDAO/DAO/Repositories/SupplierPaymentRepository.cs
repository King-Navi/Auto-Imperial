using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using AutoImperialDAO.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    public class SupplierPaymentRepository : BaseRepository<Proveedor>, ISupplierPaymentRepository
    {
        public SupplierPaymentRepository(AutoImperialContext context) : base(context)
        {
        }

        public async Task<List<CompraProveedor>> GetPaymentsBySupplierIdAsync(int supplierId)
        {
            try
            {
                Validator.IsIdValid(supplierId);

                var purchases = await _context.CompraProveedores
                    .Where(c => c.idProveedor == supplierId)
                    .ToListAsync();

                if (purchases == null || purchases.Count == 0)
                    throw new KeyNotFoundException($"No se encontraron compras para el proveedor con ID {supplierId}.");

                return purchases;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetPurchasesBySupplierIdAsync: {ex.Message}");
                return new List<CompraProveedor> { new CompraProveedor { idCompraProveedor = -1 } };
            }
        }

    }
}
