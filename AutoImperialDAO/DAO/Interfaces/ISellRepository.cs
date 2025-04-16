using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
