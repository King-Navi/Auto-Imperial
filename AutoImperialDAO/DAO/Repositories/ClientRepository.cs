using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using AutoImperialDAO.Utilities;
using Microsoft.Data.SqlClient;
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
                var client = _context.Clientes.Find(id);
                if (client == null)
                {
                    throw new ArgumentNullException("Client not found");
                }
                client.estado = AccountStatusEnum.Eliminado.ToString();
                _context.Clientes.Update(client);
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
                var searchedClient = _context.Clientes.Find(client.idCliente);
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
                _context.Clientes.Add(client);
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
                result = await _context.Clientes.FirstOrDefaultAsync(
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
            List<Cliente> result = new List<Cliente>();
            try
            {
                if (String.IsNullOrEmpty(parameter)
                    || String.IsNullOrWhiteSpace(parameter))
                {
                    throw new ArgumentException("parameter null");
                }
                result = await _context.Clientes
                           .Where(c =>
                            c.estado == statusEnum.ToString() &&
                            (c.CURP.ToLower() == parameter ||
                             (c.RFC ?? string.Empty).ToLower() == parameter ||
                             c.nombre.ToLower() == parameter ||
                             c.apellidoPaterno.ToLower() == parameter ||
                             c.apellidoMaterno.ToLower() == parameter))
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
                result = await _context.Clientes.FirstOrDefaultAsync(c => c.idCliente == id && c.estado == statusEnum.ToString());

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
                    var clientes = await _context.Clientes
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
                && !_context.Clientes.Any(c => c.CURP == client.CURP && c.idCliente != client.idCliente);
        }

        private bool ValidateRFC(Cliente client)
        {
            return !String.IsNullOrEmpty(client.RFC)
                && !String.IsNullOrWhiteSpace(client.RFC)
                && !_context.Clientes.Any(c => c.RFC == client.RFC && c.idCliente != client.idCliente);
        }
    }
}
