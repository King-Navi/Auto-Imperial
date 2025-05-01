using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.DAO.ModelsDTO;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoImperialDAO.DAO.Repositories
{
    public class VehicleRepository : BaseRepository<Vehiculo>, IVehicleRepository
    {
        public VehicleRepository(AutoImperialContext context) : base(context)
        {
        }

        public async Task<List<Marca>> GetAllBranchAsync()
        {
            List<Marca> result = new();
            try
            {
                result = await _context.Marca.ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAllAsync: {ex.Message}");
                result = new List<Marca>();
            }

            return result;
        }

        public async Task<List<Modelo>> GetModelsByBrandIdAsync(int idMarca)
        {
            List<Modelo> result = new();
            try
            {
                result = await _context.Modelo
                    .Where(m => m.idMarca == idMarca)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetModelsByBrandIdAsync: {ex.Message}");
                result = new List<Modelo>();
            }

            return result;
        }

        public async Task<List<AutoImperialDAO.Models.Version>> GetVersionsByModelIdAsync(int idModelo)
        {
            List<AutoImperialDAO.Models.Version> result = new();
            try
            {
                result = await _context.Version
                    .Where(v => v.idModelo == idModelo)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetVersionsByModelIdAsync: {ex.Message}");
                result = new List<AutoImperialDAO.Models.Version>();
            }

            return result;
        }

        public async Task<bool> RegisterVehicleAsync(Vehiculo vehiculo)
        {
            bool result = false;
            try
            {
                if (vehiculo == null)
                {
                    throw new ArgumentNullException(nameof(vehiculo));
                }

                _context.Vehiculo.Add(vehiculo);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en RegisterAsync Vehiculo: {ex.InnerException?.Message ?? ex.Message}");
            }

            return result;
        }

        public async Task<List<Vehiculo>> SearchVehicleAsync(string search, VehicleStatus statusEnum)
        {
            List<Vehiculo> result = new();

            try
            {
                if (string.IsNullOrWhiteSpace(search))
                    throw new ArgumentException("Parámetro de búsqueda inválido.");

                search = search.ToLower();

                result = await _context.Vehiculo
                    .Include(v => v.idVersionNavigation)
                        .ThenInclude(ver => ver.idModeloNavigation)
                            .ThenInclude(mod => mod.idMarcaNavigation)
                    .Include(v => v.Fotos)
                    .Where(v =>
                        v.estadoVehiculo == statusEnum.ToString() &&
                        (
                            v.VIN.ToLower().Contains(search) ||
                            v.idVersionNavigation.idModeloNavigation.nombre.ToLower().Contains(search) ||
                            v.idVersionNavigation.idModeloNavigation.idMarcaNavigation.nombre.ToLower().Contains(search)
                        )
                    )
                    .ToListAsync();

                if (result == null || result.Count == 0)
                    throw new KeyNotFoundException("No se encontraron vehículos que coincidan con la búsqueda.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SearchVehicleAsync: {ex.Message}");
                return new List<Vehiculo>();
            }

            return result;
        }

        public async Task<bool> EditVehicleAsync(Vehiculo vehiculo)
        {
            bool result = false;
            try
            {
                if (vehiculo == null)
                    throw new ArgumentNullException(nameof(vehiculo));

                var existingVehicle = await _context.Vehiculo
                    .Include(v => v.Fotos)
                    .FirstOrDefaultAsync(v => v.idVehiculo == vehiculo.idVehiculo);

                if (existingVehicle == null)
                    throw new ArgumentException("Vehículo no encontrado");

                _context.Entry(existingVehicle).CurrentValues.SetValues(vehiculo);

                _context.Entry(existingVehicle).Property(v => v.idVehiculo).IsModified = false;
                _context.Entry(existingVehicle).Property(v => v.precioProveedor).IsModified = false;
                _context.Entry(existingVehicle).Property(v => v.estadoVehiculo).IsModified = false;
                _context.Entry(existingVehicle).Property(v => v.idCompraProveedor).IsModified = false;
                _context.Entry(existingVehicle).Property(v => v.idVersion).IsModified = true;
                _context.Entry(existingVehicle).Collection(v => v.Venta).IsModified = false;
                _context.Entry(existingVehicle).Collection(v => v.idDescuento).IsModified = false;

                if (vehiculo.Fotos.Any())
                {
                    var newPhoto = vehiculo.Fotos.First();
                    if (existingVehicle.Fotos.Any())
                    {
                        existingVehicle.Fotos.First().foto = newPhoto.foto;
                        _context.Entry(existingVehicle.Fotos.First()).State = EntityState.Modified;
                    }
                    else
                    {
                        existingVehicle.Fotos.Add(new Fotos { foto = newPhoto.foto });
                    }
                }

                await _context.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditVehicleAsync: {ex.Message}");
            }

            return result;
        }

        public bool DeleteById(int id)
        {
            bool result = false;
            try
            {
                var vehicle = _context.Vehiculo.Find(id);
                if (vehicle == null)
                {
                    throw new ArgumentNullException("Vehicle not found");
                }

                vehicle.estadoVehiculo = VehicleStatus.Eliminado.ToString();
                _context.Vehiculo.Update(vehicle);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error en DeleteById Vehiculo: {ex.Message}");
            }
            return result;
        }

        public List<Vehiculo> AdvancedSearchVehicle(Utilities.VehicleSearch search, VehicleStatus statusEnum)
        {
            try
            {
                var query = _context.Vehiculo
                    .Include(v => v.idVersionNavigation)
                        .ThenInclude(ver => ver.idModeloNavigation)
                            .ThenInclude(mod => mod.idMarcaNavigation)
                    .Include(v => v.Fotos)
                    .Where(v => v.estadoVehiculo == statusEnum.ToString())
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(search.SearchTerm))
                {
                    string searchTerm = search.SearchTerm.ToLower();
                    query = query.Where(v =>
                        v.VIN.ToLower().Contains(searchTerm) ||
                        v.idVersionNavigation.idModeloNavigation.nombre.ToLower().Contains(searchTerm) ||
                        v.idVersionNavigation.idModeloNavigation.idMarcaNavigation.nombre.ToLower().Contains(searchTerm));
                }

                if (!string.IsNullOrWhiteSpace(search.Color))
                {
                    string color = search.Color.ToLower();
                    query = query.Where(v => v.color.ToLower().Contains(color));
                }

                if (!string.IsNullOrWhiteSpace(search.Version))
                {
                    string version = search.Version.ToLower();
                    query = query.Where(v => v.idVersionNavigation.nombre.ToLower().Contains(version));
                }

                if (!string.IsNullOrWhiteSpace(search.Year) && int.TryParse(search.Year, out int parsedYear))
                {
                    query = query.Where(v => v.anio == parsedYear);
                }

                if (search.MinPrice.HasValue && search.MinPrice > 0)
                {
                    query = query.Where(v => v.precioVehiculo >= search.MinPrice.Value);
                }

                if (search.MaxPrice.HasValue && search.MaxPrice > 0)
                {
                    query = query.Where(v => v.precioVehiculo <= search.MaxPrice.Value);
                }

                var result = query.ToList();

                if (result == null || result.Count == 0)
                    throw new KeyNotFoundException("No se encontraron vehículos que coincidan con los filtros.");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AdvancedSearchVehicle: {ex.Message}");
                return new List<Vehiculo>();
            }
        }

        public List<InventoryItem> GetCurrentInventory(DateTime startDate, DateTime endDate)
        {
            try
            {
                var start = DateOnly.FromDateTime(startDate);
                var end = DateOnly.FromDateTime(endDate);
                var inventory = from vehiculo in _context.Vehiculo
                                join compra in _context.CompraProveedor on vehiculo.idCompraProveedor equals compra.idCompraProveedor
                                join version in _context.Version on vehiculo.idVersion equals version.idVersion
                                join modelo in _context.Modelo on version.idModelo equals modelo.idModelo
                                join marca in _context.Marca on modelo.idMarca equals marca.idMarca
                                where (vehiculo.estadoVehiculo == VehicleStatus.Disponible.ToString() ||
                                       vehiculo.estadoVehiculo == VehicleStatus.Reservado.ToString())
                                      && compra.fechaCompra >= start
                                      && compra.fechaCompra <= end
                                select new InventoryItem
                                {
                                    Model = modelo.nombre,
                                    Version = version.nombre,
                                    Brand = marca.nombre
                                };

                return inventory.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
