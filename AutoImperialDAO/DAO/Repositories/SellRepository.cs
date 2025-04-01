using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
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
            //TODO
            throw new NotImplementedException();
        }

        public bool Edit(Venta venta)
        {
            //TODO
            throw new NotImplementedException();
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
                    v.idVehiculoNavigation.VIN.ToLower().Contains(parameter) ||
                    (
                        (v.idReservaNavigation.idClienteNavigation.nombre + " " +
                         v.idReservaNavigation.idClienteNavigation.apellidoPaterno + " " +
                         v.idReservaNavigation.idClienteNavigation.apellidoMaterno)
                        .ToLower().Contains(parameter)
                    )
                )
                .ToListAsync();

            return ventasFiltradas;
        }
    }
}
