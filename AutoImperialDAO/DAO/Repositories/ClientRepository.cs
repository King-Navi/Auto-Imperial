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
        const int MAX_PAGE_SIZE = 100;
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

        public bool EditById(Cliente client)
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
                _context.Clientes.Add(client);
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
                client.estado = AccountStatusEnum.Activa.ToString();
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

        public Cliente SearchById(int id)
        {
            Cliente? result = new Cliente();
            try
            {
                Validator.IsIdValid(id);
                result = _context.Clientes.Find(id);
                if (result == null)
                {
                    throw new KeyNotFoundException($"No se encontró un cliente con ID {id}");
                }
            }
            catch (Exception)
            {
                result = new Cliente { idCliente = -1 };
            }
            return result;
        }

        public List<Cliente> SearchById(List<int> ids, int page, int pageSize = 50)
        {
            pageSize = Math.Min(pageSize, MAX_PAGE_SIZE);
            List<Cliente> result = new List<Cliente>();
            try
            {
                if (ids == null || ids.Count == 0)
                    throw new ArgumentException("La lista de IDs no puede estar vacía.");

                if (ids.Count > 1000)
                    throw new ArgumentException("No se pueden buscar más de 1000 IDs a la vez.");

                result = _context.Clientes
                    .Where(c => ids.Contains(c.idCliente))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

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
            if (String.IsNullOrEmpty(client.CURP) || String.IsNullOrWhiteSpace(client.CURP)
                || _context.Clientes.Any(c => c.CURP == client.CURP))
            {
                throw new ArgumentNullException("Client error in CURP");
            }

            if (String.IsNullOrEmpty(client.RFC) || String.IsNullOrWhiteSpace(client.RFC)
                || _context.Clientes.Any(c => c.RFC == client.RFC))
            {
                throw new ArgumentNullException("Client error in RFC");
            }
            return !string.IsNullOrEmpty(client.nombre)
                && !string.IsNullOrEmpty(client.apellidoMaterno)
                && !string.IsNullOrEmpty(client.apellidoPaterno)
                && !string.IsNullOrEmpty(client.codigoPostal)
                && !string.IsNullOrEmpty(client.telefono)
                && !string.IsNullOrEmpty(client.correo);
        }
    }
}
