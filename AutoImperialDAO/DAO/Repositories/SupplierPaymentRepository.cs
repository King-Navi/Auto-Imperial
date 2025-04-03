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

        public async Task<bool> RegisterSupplierPaymentAsync(CompraProveedor nuevaCompra)
        {
            try
            {
                Validator.IsIdValid(nuevaCompra.idProveedor);
                Validator.IsIdValid(nuevaCompra.idAdministrador);
                nuevaCompra.fechaCompra = DateOnly.FromDateTime(DateTime.Now);

                await _context.CompraProveedor.AddAsync(nuevaCompra);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar compra: {ex.Message}");
                return false;
            }
        }


        public async Task<List<CompraProveedor>> GetPaymentsBySupplierIdAsync(int supplierId)
        {
            try
            {
                Validator.IsIdValid(supplierId);

                var purchases = await _context.CompraProveedor
                    .Where(c => c.idProveedor == supplierId)
                    .ToListAsync();

                return purchases;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetPurchasesBySupplierIdAsync: {ex.Message}");
                return new List<CompraProveedor> { new CompraProveedor { idCompraProveedor = -1 } };
            }
        }

        public int GetCountVehiclesById(int supplierPaymentId)
        {
            try
            {
                Validator.IsIdValid(supplierPaymentId);
                return _context.Vehiculo.Count(vehicle => vehicle.idCompraProveedor == supplierPaymentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetCountVehiclesById: {ex.Message}");
                return -1;
            }
        }
    }
}
