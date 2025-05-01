using AutoImperialDAO.DAO.ModelsDTO;
using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface ISupplierPaymentRepository
    {
        Task<bool> RegisterSupplierPaymentAsync(CompraProveedor nuevaCompra);
        Task<List<CompraProveedor>> GetPaymentsBySupplierIdAsync(int supplierId);
        int GetCountVehiclesById(int supplierPaymentId);
        List<FinancialPurchaseDTO> GetFinancialPurchases(DateTime startDate, DateTime endDate);
    }
}
