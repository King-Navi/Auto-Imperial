using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
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

        public Venta? GetSellByIdReserve(int idReserve)
        {
            try
            {
                return _context.Venta
                    .Where(v => v.idReserva == idReserve)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetSellByIdReserve: {ex.Message}");
            }
            return null;
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

        public Vehiculo? GetAvailableVehicleByVersion(int idVersion)
        {
            try
            {
                return _context.Vehiculo
                    .Where(v => v.idVersion == idVersion && v.estadoVehiculo == VehicleStatus.Disponible.ToString())
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAvailableVehicleByVersion: {ex.Message}");
                return null;
            }
        }

        public bool RegisterSaleWithStockCheck(Venta venta, int idVersion)
        {
            bool result = false;
            try
            {
                var availableVehicle = GetAvailableVehicleByVersion(idVersion);
                if (availableVehicle == null)
                {
                    Console.WriteLine("No hay vehículos disponibles para la versión solicitada.");
                    return false;
                }

                venta.idVehiculo = availableVehicle.idVehiculo;
                venta.estadoVenta = "Registrada";
                venta.fechaVenta = DateOnly.FromDateTime(DateTime.Today);

                _context.Venta.Add(venta);

                availableVehicle.estadoVehiculo = VehicleStatus.Vendido.ToString();
                _context.Vehiculo.Update(availableVehicle);

                _context.SaveChanges();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RegisterSaleWithStockCheck: {ex.Message}");
            }

            return result;
        }

    }
}
