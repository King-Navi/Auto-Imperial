using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.DAO.ModelsDTO;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using AutoImperialDAO.Utilities;
using Microsoft.EntityFrameworkCore;

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
        public List<SaleData> GetSalesReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var start = DateOnly.FromDateTime(startDate);
                var end = DateOnly.FromDateTime(endDate);

                var sales = from venta in _context.Venta
                            join reserva in _context.Reserva on venta.idReserva equals reserva.idReserva
                            join vendedor in _context.Vendedor on reserva.idVendedor equals vendedor.idVendedor
                            join cliente in _context.Cliente on reserva.idCliente equals cliente.idCliente
                            join vehiculo in _context.Vehiculo on venta.idVehiculo equals vehiculo.idVehiculo
                            join version in _context.Version on vehiculo.idVersion equals version.idVersion
                            join modelo in _context.Modelo on version.idModelo equals modelo.idModelo
                            join marca in _context.Marca on modelo.idMarca equals marca.idMarca
                            where venta.estadoVenta != "Eliminada"
                                  && venta.fechaVenta >= start
                                  && venta.fechaVenta <= end
                            select new SaleData
                            {
                                Salesperson = vendedor.nombre + " " + vendedor.apellidoPaterno + " " + vendedor.apellidoMaterno,
                                Customer = cliente.nombre + " " + cliente.apellidoPaterno + " " + cliente.apellidoMaterno,
                                Vehicle = $"{vehiculo.anio} {vehiculo.color} {marca.nombre} {modelo.nombre} {version.nombre} {version.motor}",
                                Type = venta.formaPago,
                                Amount = venta.precioVehiculo
                            };

                return sales.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<FinancialSaleDTO> GetFinancialSales(DateTime startDate, DateTime endDate)
        {
            try
            {
                var start = DateOnly.FromDateTime(startDate);
                var end = DateOnly.FromDateTime(endDate);

                var sales = from venta in _context.Venta
                            join vehiculo in _context.Vehiculo on venta.idVehiculo equals vehiculo.idVehiculo
                            join version in _context.Version on vehiculo.idVersion equals version.idVersion
                            join modelo in _context.Modelo on version.idModelo equals modelo.idModelo
                            join marca in _context.Marca on modelo.idMarca equals marca.idMarca
                            where venta.fechaVenta >= start &&
                                  venta.fechaVenta <= end &&
                                  venta.estadoVenta != "Eliminada"
                            select new FinancialSaleDTO
                            {
                                Model = modelo.nombre,
                                Version = version.nombre,
                                Brand = marca.nombre,
                                Amount = vehiculo.precioVehiculo ?? 0
                            };

                return sales.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

   
}
