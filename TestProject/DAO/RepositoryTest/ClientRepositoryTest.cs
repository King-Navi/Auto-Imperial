using AutoImperialDAO.DAO.Repositories;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

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
            var options = new DbContextOptionsBuilder<AutoImperialContext>()
                .UseSqlServer(TestAssemblyInitializer.MsSqlContainer.GetConnectionString())
                .Options;
            _repository = new ClientRepository(new AutoImperialContext(options));

        }

        [TestMethod]
        public void Register_WhenValidClient_ShouldReturnTrue()
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
    }
}
