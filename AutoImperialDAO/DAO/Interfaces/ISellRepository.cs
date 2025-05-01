using AutoImperialDAO.DAO.ModelsDTO;
using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface ISellRepository
    {
        Venta? GetSellByIdReserve(int idReserve);
        bool RegisterSaleWithStockCheck(Venta venta, int idVersion);
        bool DeleteById(int id);
        bool Edit(Venta venta);
        Task<List<Venta>> SearchByVINClientAsync(string parameter);
        Vehiculo? GetAvailableVehicleByVersion(int idVersion);
        List<SaleData> GetSalesReport(DateTime startDate, DateTime endDate);
        List<FinancialSaleDTO> GetFinancialSales(DateTime startDate, DateTime endDate);
    }
}
