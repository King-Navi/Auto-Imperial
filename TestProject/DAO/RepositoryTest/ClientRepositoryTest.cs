using AutoImperialDAO.DAO.Repositories;
using AutoImperialDAO.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using TestProject.DAO.Utilities;

namespace TestProject.DAO.RepositoryTest
{
    [TestCategory("ClientRepositoryTest")]
    [TestClass]
    public class ClientRepositoryTest
    {
        private ClientRepository _repository;

        [TestInitialize]
        public void Setup()
        {

            var builder = new SqlConnectionStringBuilder(TestAssemblyInitializer.MsSqlContainer.GetConnectionString())
            {
                InitialCatalog = "AutoImperial",
                UserID = "sa",
                Password = Constants.CONTRASENIA_PRUEBA
            };
            var forcedConnectionString = builder.ToString();
            Console.WriteLine("Using database connection: " + forcedConnectionString);
            var options = new DbContextOptionsBuilder<AutoImperialContext>()
                .UseSqlServer(forcedConnectionString)
                .Options;

            var _context = new AutoImperialContext(options);
            _repository = new ClientRepository(_context);

        }

        [TestMethod]
        public void Register_ValidClient_ReturnTrue()
        {
            var client = new Cliente
            {
                nombre = "Juan",
                apellidoPaterno = "Pérez",
                apellidoMaterno = "Gómez",
                telefono = "1234567890",
                codigoPostal = "12345",
                correo = "juan@example.com",
                CURP = "GOMJ850123HDFRLR05",
                RFC = "GOMJ850123MNE"
            };

            bool result = _repository.Register(client);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Register_InvalidClient_ReturnFalse()
        {
            var client = new Cliente
            {
                nombre = "Juan",
                apellidoPaterno = "Pérez",
                apellidoMaterno = "Gómez",
                telefono = "1234567890",
                codigoPostal = "12345",
                correo = "juan@example.com",
                CURP = "", 
                RFC = ""   
            };

            bool result = _repository.Register(client);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Register_ClientAlreadyExists_ReturnFalse()
        {
            var existingClient = new Cliente
            {
                nombre = "Carlos",
                apellidoPaterno = "Hernández",
                apellidoMaterno = "Lopez",
                telefono = "5555555555",
                codigoPostal = "67890",
                correo = "carlos@example.com",
                CURP = "HERL900101HDFRLR08",
                RFC = "HERL900101MN1"
            };

            Assert.IsTrue(_repository.Register(existingClient));

            var duplicateClient = new Cliente
            {
                nombre = "Carlos",
                apellidoPaterno = "Hernández",
                apellidoMaterno = "Lopez",
                telefono = "5555555555",
                codigoPostal = "67890",
                correo = "carlos@example.com",
                CURP = "HERL900101HDFRLR08", 
                RFC = "HERL900101MN1"       
            };

            bool result = _repository.Register(duplicateClient);

            Assert.IsFalse(result);
        }
    }
}
