using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    public class SupplierRepository : BaseRepository<Proveedor>, ISupplierRepository
    {
        public SupplierRepository(AutoImperialContext context) : base(context)
        {
        }

        public bool Register(Proveedor proveedor)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Proveedor>> SearchByNameCityAsync(string parameter, AccountStatusEnum statusEnum)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                throw new ArgumentException("Search parameter cannot be null or whitespace.", nameof(parameter));

            parameter = parameter.Trim().ToLower();

            try
            {
                var result = await _context.Proveedores
                    .Where(p =>
                        (p.estado ?? string.Empty).ToLower() == statusEnum.ToString().ToLower() &&
                        (
                            p.nombreProveedor.ToLower().Contains(parameter) ||
                            (p.ciudad ?? string.Empty).ToLower().Contains(parameter)
                        )
                    )
                    .ToListAsync();

                if (result == null || result.Count == 0)
                    throw new KeyNotFoundException("No suppliers found matching the criteria.");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchByNameCityAsync: {ex.Message}");
                return new List<Proveedor> { new Proveedor { idProveedor = -1 } };
            }
        }

        public Task<Proveedor> SearchByIdAsync(int id, AccountStatusEnum statusEnum)
        {
            throw new NotImplementedException();
        }
    }
}
