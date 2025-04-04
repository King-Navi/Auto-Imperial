﻿using AutoImperialDAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface ISupplierPaymentRepository
    {
        Task<bool> RegisterSupplierPaymentAsync(CompraProveedor nuevaCompra);
        Task<List<CompraProveedor>> GetPaymentsBySupplierIdAsync(int supplierId);
        int GetCountVehiclesById(int supplierPaymentId);
    }
}
