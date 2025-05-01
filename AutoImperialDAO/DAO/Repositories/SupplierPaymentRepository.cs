using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.DAO.ModelsDTO;
using AutoImperialDAO.Models;
using AutoImperialDAO.Utilities;
using Microsoft.EntityFrameworkCore;

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
                throw;
            }
        }
        public List<FinancialPurchaseDTO> GetFinancialPurchases(DateTime startDate, DateTime endDate)
        {
            try
            {
                var start = DateOnly.FromDateTime(startDate);
                var end = DateOnly.FromDateTime(endDate);

                var purchases = from vehiculo in _context.Vehiculo
                                join version in _context.Version on vehiculo.idVersion equals version.idVersion
                                join modelo in _context.Modelo on version.idModelo equals modelo.idModelo
                                join marca in _context.Marca on modelo.idMarca equals marca.idMarca
                                join compra in _context.CompraProveedor on vehiculo.idCompraProveedor equals compra.idCompraProveedor
                                join proveedor in _context.Proveedor on compra.idProveedor equals proveedor.idProveedor
                                where compra.fechaCompra >= start &&
                                      compra.fechaCompra <= end
                                select new FinancialPurchaseDTO
                                {
                                    Model = modelo.nombre,
                                    Version = version.nombre,
                                    Brand = marca.nombre,
                                    Supplier = proveedor.nombreProveedor,
                                    Amount = vehiculo.precioProveedor ?? 0
                                };

                return purchases.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
