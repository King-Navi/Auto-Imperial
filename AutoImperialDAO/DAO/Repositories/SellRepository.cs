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
    public class SellRepository : BaseRepository<Venta>, ISellRepository
    {
        public SellRepository(AutoImperialContext context) : base(context)
        {
        }
        public bool DeleteById(int id)
        {
            var venta = _context.Venta.FirstOrDefault(v => v.idVenta == id);
            if (venta == null)
            {
                return false;
            }

            venta.estadoVenta = "Eliminada";
            _context.Venta.Update(venta);

            return _context.SaveChanges() > 0;
        }

        public bool Edit(Venta venta)
        {
            bool result = false;
            try
            {
                Validator.IsIdValid(venta.idVenta);
                var searchedVenta = _context.Venta.Find(venta.idVenta);
                if (searchedVenta == null)
                {
                    throw new ArgumentNullException("Venta not found");
                }

                searchedVenta.precioVehiculo = venta.precioVehiculo;
                searchedVenta.notasAdicionales= venta.notasAdicionales;
                searchedVenta.formaPago = venta.formaPago;

                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit Venta: {ex.Message}");
            }

            return result;
        }




        public bool Register(Venta venta)
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<List<Venta>> SearchByVINClientAsync(string parameter)
        {
            parameter = parameter.ToLower();

            var ventasFiltradas = await _context.Venta
                .Include(v => v.idVehiculoNavigation)
                    .ThenInclude(veh => veh.idVersionNavigation)
                        .ThenInclude(ver => ver.idModeloNavigation)
                            .ThenInclude(mod => mod.idMarcaNavigation)
                .Include(v => v.idReservaNavigation)
                    .ThenInclude(r => r.idClienteNavigation)
                .Include(v => v.idReservaNavigation)
                    .ThenInclude(r => r.idVendedorNavigation)
                .Where(v =>
                    (v.idVehiculoNavigation.VIN.ToLower().Contains(parameter) ||
                    (
                        (v.idReservaNavigation.idClienteNavigation.nombre + " " +
                         v.idReservaNavigation.idClienteNavigation.apellidoPaterno + " " +
                         v.idReservaNavigation.idClienteNavigation.apellidoMaterno)
                        .ToLower().Contains(parameter)
                    )) && v.estadoVenta != "Eliminada"
                )
                .ToListAsync();

            return ventasFiltradas;
        }
    }
}
