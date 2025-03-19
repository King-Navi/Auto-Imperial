using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using AutoImperialDAO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoImperialDAO.DAO.Repositories
{
    public class EmployeeRepository : BaseRepository<Vendedor>, IEmployeeRepository
    {
        const int MAX_PAGES = 20;
        const int MAX_SEARCH = 100;
        public EmployeeRepository(AutoImperialContext context) : base(context)
        {
        }

        public bool DeleteById(int id)
        {
            bool result = false;
            try
            {
                Validator.IsIdValid(id);
                var employee = _context.Vendedores.Find(id);
                if (employee == null)
                {
                    throw new ArgumentNullException("Employee not found");
                }
                employee.estadoCuenta = AccountStatusEnum.Eliminado.ToString();
                _context.Vendedores.Update(employee);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }

        public bool Edit(Vendedor employee)
        {
            bool result = false;
            try
            {
                Validator.IsIdValid(employee.idVendedor);
                var searchedEmployee = _context.Vendedores.Find(employee.idVendedor);
                if (searchedEmployee == null)
                {
                    throw new ArgumentNullException("Employee not found");
                }
                if (!IsEmployeeValid(employee))
                {
                    throw new ArgumentException("Employee is not valid");
                }
                _context.Entry(searchedEmployee).CurrentValues.SetValues(employee);
                _context.Entry(searchedEmployee).Property(x => x.password).IsModified = false;
                _context.Entry(searchedEmployee).Property(x => x.estadoCuenta).IsModified = false;
                _context.Entry(searchedEmployee).Property(x => x.idVendedor).IsModified = false;
                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Edit: {ex.Message}");
            }

            return result;
        }

        public bool Register(Vendedor employee)
        {
            bool result = false;
            try
            {
                if (!IsEmployeeValid(employee))
                {
                    throw new ArgumentException("Employee is not valid");
                }
                employee.estadoCuenta = AccountStatusEnum.Activo.ToString();
                _context.Vendedores.Add(employee);
                _context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Register: {ex.InnerException?.Message ?? ex.Message}");
            }
            return result;
        }

        public async Task<Vendedor> SearchByCURPAsync(string CURP, AccountStatusEnum statusEnum)
        {
            Vendedor? result = null;
            try
            {
                if (String.IsNullOrEmpty(CURP)
                    || String.IsNullOrWhiteSpace(CURP))
                {
                    throw new ArgumentException("CURP null");
                }
                result = await _context.Vendedores.FirstOrDefaultAsync(
                    c => c.CURP.ToLower() == CURP.ToLower()
                    && c.estadoCuenta == statusEnum.ToString());
                if (result == null)
                {
                    throw new KeyNotFoundException($"No se encontró un empleado con ID {CURP}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SearchByCURP: {ex.Message}");
                result = new Vendedor { idVendedor = -1 };
            }
            return result;
        }

        public async Task<List<Vendedor>> SearchByCurpRfcNameAsync(string parameter, AccountStatusEnum statusEnum)
        {
            List<Vendedor> result = new List<Vendedor>();
            try
            {
                if (string.IsNullOrEmpty(parameter) || string.IsNullOrWhiteSpace(parameter))
                {
                    throw new ArgumentException("parameter null");
                }

                parameter = parameter.ToLower();

                result = await _context.Vendedores
                    .Where(c =>
                        c.estadoCuenta == statusEnum.ToString() &&
                        (
                            c.CURP.ToLower() == parameter ||
                            (c.RFC ?? string.Empty).ToLower() == parameter ||
                            ((c.nombre + " " + c.apellidoPaterno + " " + c.apellidoMaterno).ToLower()).Contains(parameter)
                        )
                    )
                    .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    throw new KeyNotFoundException("No se encontró un empleado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SearchByCurpRfcNameAsync: {ex.Message}");
                return new List<Vendedor> { new Vendedor { idVendedor = -1 } };
            }
            return result;
        }

        public async Task<Vendedor> SearchByIdAsync(int id, AccountStatusEnum statusEnum)
        {
            Vendedor? result = new Vendedor();
            try
            {
                Validator.IsIdValid(id);
                result = await _context.Vendedores.FirstOrDefaultAsync(c => c.idVendedor == id && c.estadoCuenta == statusEnum.ToString());

                if (result == null)
                {
                    throw new KeyNotFoundException($"No se encontró un empleado con ID {id}");
                }

                return result;
            }
            catch (Exception)
            {
                result = new Vendedor { idVendedor = -1 };
            }
            return result;
        }

        public async Task<List<Vendedor>> SearchByPagesAsync(int startPage, int totalPages, AccountStatusEnum status, int pageSize = 50)
        {
            totalPages = Math.Min(totalPages, MAX_PAGES);
            pageSize = Math.Min(pageSize, MAX_SEARCH);

            List<Vendedor> result = new List<Vendedor>();

            try
            {
                if (startPage <= 0)
                {
                    throw new ArgumentException("El parámetro startPage debe ser mayor o igual a 1.");
                }
                for (int i = 0; i < totalPages; i++)
                {
                    int currentPage = startPage + i;
                    var employees = await _context.Vendedores
                        .Where(c => c.estadoCuenta.ToLower() == status.ToString().ToLower())
                        .Skip((currentPage - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    if (employees.Count == 0)
                        break;

                    result.AddRange(employees);
                }
            }
            catch (Exception)
            {
                result = new List<Vendedor>();
            }

            return result;
        }

        private bool IsEmployeeValid(Vendedor employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException();
            }
            if (!ValidateCURP(employee))
            {
                throw new ArgumentException("Client error in CURP", nameof(employee.CURP));
            }

            if (!ValidateRFC(employee))
            {
                throw new ArgumentException("Client error in RFC", nameof(employee.RFC));
            }
            return !string.IsNullOrEmpty(employee.nombre)
                && !string.IsNullOrEmpty(employee.apellidoMaterno)
                && !string.IsNullOrEmpty(employee.apellidoPaterno)
                && !string.IsNullOrEmpty(employee.codigoPostal)
                && !string.IsNullOrEmpty(employee.telefono)
                && !string.IsNullOrEmpty(employee.correo);
        }

        private bool ValidateCURP(Vendedor employee)
        {
            return !String.IsNullOrEmpty(employee.CURP)
                && !String.IsNullOrWhiteSpace(employee.CURP)
                && !_context.Vendedores.Any(c => c.CURP == employee.CURP && c.idVendedor != employee.idVendedor);
        }

        private bool ValidateRFC(Vendedor employee)
        {
            return !String.IsNullOrEmpty(employee.RFC)
                && !String.IsNullOrWhiteSpace(employee.RFC)
                && !_context.Vendedores.Any(c => c.RFC == employee.RFC && c.idVendedor != employee.idVendedor);
        }
    }
}
