﻿using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.DAO.ModelsDTO;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using AutoImperialDAO.Utilities;
using Microsoft.EntityFrameworkCore;
namespace AutoImperialDAO.DAO.Repositories
{
    public class ClientRepository : BaseRepository<Cliente>, IClientRepository
    {
        const int MAX_PAGES = 20;
        const int MAX_SEARCH = 100;
        public ClientRepository(AutoImperialContext context) : base(context)
        {
        }

        public bool DeleteById(int id)
        {
            bool result = false;
            try
            {
                Validator.IsIdValid(id);
                var client = _context.Cliente.Find(id);
                if (client == null)
                {
                    throw new ArgumentNullException("Client not found");
                }
                client.estado = AccountStatusEnum.Eliminado.ToString();
                _context.Cliente.Update(client);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }

        public bool Edit(Cliente client)
        {
            bool result = false;
            try
            {
                Validator.IsIdValid(client.idCliente);
                var searchedClient = _context.Cliente.Find(client.idCliente);
                if (searchedClient == null)
                {
                    throw new ArgumentNullException("Client not found");
                }
                if (!IsClientValid(client))
                {
                    throw new ArgumentException("Client is not valid");
                }
                _context.Entry(searchedClient).CurrentValues.SetValues(client);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
            }

            return result;
        }

        public bool Register(Cliente client)
        {
            bool result = false;
            try
            {
                if (!IsClientValid(client))
                {
                    throw new ArgumentException("Client is not valid");
                }
                client.estado = AccountStatusEnum.Activo.ToString();
                _context.Cliente.Add(client);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Register: {ex.InnerException?.Message ?? ex.Message}");
            }
            return result;
        }

        public async Task<Cliente> SearchByCURPAsync(string CURP, AccountStatusEnum statusEnum)
        {
            Cliente? result = null;
            try
            {
                if (String.IsNullOrEmpty(CURP)
                    || String.IsNullOrWhiteSpace(CURP))
                {
                    throw new ArgumentException("CURP null");
                }
                result = await _context.Cliente.FirstOrDefaultAsync(
                    c => c.CURP.ToLower() == CURP.ToLower() 
                    && c.estado == statusEnum.ToString());
                if (result == null)
                {
                    throw new KeyNotFoundException($"No se encontró un cliente con ID {CURP}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SearchByCURP: {ex.Message}");
                result = new Cliente { idCliente = -1 };
            }
            return result;
        }

        public async Task<List<Cliente>> SearchByCurpRfcNameAsync(string parameter, AccountStatusEnum statusEnum)
        {
            string estadoStr = statusEnum.ToString(); 
            List<Cliente> result = new List<Cliente>();
            try
            {
                if (String.IsNullOrEmpty(parameter)
                    || String.IsNullOrWhiteSpace(parameter))
                {
                    throw new ArgumentException("parameter null");
                }
                result = await _context.Cliente
                                    .Where(c =>
                                        c.estado.ToUpper() == estadoStr.ToUpper() &&
                                        (
                                            EF.Functions.Like(c.CURP.Trim().ToUpper(), $"%{parameter.Trim().ToUpper()}%") ||
                                            EF.Functions.Like((c.RFC ?? string.Empty).Trim().ToUpper(), $"%{parameter.Trim().ToUpper()}%") ||
                                            EF.Functions.Like((c.nombre + " " + c.apellidoPaterno + " " + c.apellidoMaterno).ToUpper(), $"%{parameter.Trim().ToUpper()}%")
                                        ))
                                    .ToListAsync();
                if (result == null || result.Count == 0)
                {
                    throw new KeyNotFoundException($"No se encontró un clientes");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SearchByCurpRfcNameAsync: {ex.Message}");
                return new List<Cliente> { new Cliente { idCliente = -1 } };
            }
            return result;
        }

        public async Task<Cliente> SearchByIdAsync(int id, AccountStatusEnum statusEnum)
        {
            Cliente? result = new Cliente();
            try
            {
                Validator.IsIdValid(id);
                result = await _context.Cliente.FirstOrDefaultAsync(c => c.idCliente == id && c.estado == statusEnum.ToString());

                if (result == null)
                {
                    throw new KeyNotFoundException($"No se encontró un cliente con ID {id}");
                }

                return result;
            }
            catch (Exception)
            {
                result = new Cliente { idCliente = -1 };
            }
            return result;
        }

        public async Task<List<Cliente>> SearchByPagesAsync(int startPage, int totalPages, AccountStatusEnum status, int pageSize = 50)
        {
            totalPages = Math.Min(totalPages, MAX_PAGES);
            pageSize = Math.Min(pageSize, MAX_SEARCH);

            List<Cliente> result = new List<Cliente>();

            try
            {
                if (startPage <= 0)
                {
                    throw new ArgumentException("El parámetro startPage debe ser mayor o igual a 1.");
                }
                for (int i = 0; i < totalPages; i++)
                {
                    int currentPage = startPage + i;
                    var clientes = await _context.Cliente
                        .Where(c => c.estado.ToLower() == status.ToString().ToLower())
                        .Skip((currentPage - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    if (clientes.Count == 0)
                        break;

                    result.AddRange(clientes);
                }
            }
            catch (Exception)
            {
                result = new List<Cliente>();
            }

            return result;
        }

        private bool IsClientValid(Cliente client)
        {
            if (client == null)
            {
                throw new ArgumentNullException();
            }
            if (!ValidateCURP(client))
            {
                throw new ArgumentException("Client error in CURP", nameof(client.CURP));
            }

            if (!ValidateRFC(client))
            {
                throw new ArgumentException("Client error in RFC", nameof(client.RFC));
            }
            return !string.IsNullOrEmpty(client.nombre)
                && !string.IsNullOrEmpty(client.apellidoMaterno)
                && !string.IsNullOrEmpty(client.apellidoPaterno)
                && !string.IsNullOrEmpty(client.codigoPostal)
                && !string.IsNullOrEmpty(client.telefono)
                && !string.IsNullOrEmpty(client.correo);
        }

        private bool ValidateCURP(Cliente client)
        {
            return !String.IsNullOrEmpty(client.CURP)
                && !String.IsNullOrWhiteSpace(client.CURP)
                && !_context.Cliente.Any(c => c.CURP == client.CURP && c.idCliente != client.idCliente);
        }

        private bool ValidateRFC(Cliente client)
        {
            return !String.IsNullOrEmpty(client.RFC)
                && !String.IsNullOrWhiteSpace(client.RFC)
                && !_context.Cliente.Any(c => c.RFC == client.RFC && c.idCliente != client.idCliente);
        }
        public List<ClientPurchaseDTO> GetClientPurchases(int clientId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var start = DateOnly.FromDateTime(startDate);
                var end = DateOnly.FromDateTime(endDate);

                var purchases = from venta in _context.Venta
                                join reserva in _context.Reserva on venta.idReserva equals reserva.idReserva
                                join vehiculo in _context.Vehiculo on venta.idVehiculo equals vehiculo.idVehiculo
                                join version in _context.Version on vehiculo.idVersion equals version.idVersion
                                join modelo in _context.Modelo on version.idModelo equals modelo.idModelo
                                join marca in _context.Marca on modelo.idMarca equals marca.idMarca
                                where reserva.idCliente == clientId
                                      && venta.fechaVenta >= start
                                      && venta.fechaVenta <= end
                                      && venta.estadoVenta != "Eliminada"
                                select new ClientPurchaseDTO
                                {
                                    Model = modelo.nombre,
                                    Version = version.nombre,
                                    Brand = marca.nombre
                                };

                return purchases.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
