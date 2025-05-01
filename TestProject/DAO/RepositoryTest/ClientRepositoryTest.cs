using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.DAO.Repositories;
using AutoImperialDAO.Enums;
using AutoImperialDAO.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestProject.DAO.Utilities;

namespace TestProject.DAO.RepositoryTest
{
    [TestCategory("ClientRepositoryTest")]
    [TestClass]
    public class ClientRepositoryTest
    {
        private const int NON_EXIST_CLIENT= 99999;
        private IClientRepository _repository;

        [TestInitialize]
        public void Setup()
        {

            var builder = new SqlConnectionStringBuilder(TestAssemblyInitializer.MsSqlContainer.GetConnectionString())
            {
                InitialCatalog = "AutoImperial",
                UserID = "sa",
                Password = ConstantsTestDAO.CONTRASENIA_PRUEBA
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

        [TestMethod]
        public async Task DeleteById_ClientExists_SetEstadoToEliminado()
        {
            var client = new Cliente
            {
                nombre = "Pedro",
                apellidoPaterno = "García",
                apellidoMaterno = "Hernández",
                telefono = "5555555555",
                codigoPostal = "54321",
                correo = "pedro@example.com",
                CURP = "GARP900101HDFRLR04",
                RFC = "GARP900101MN0",
                estado = AccountStatusEnum.Activo.ToString()
            };

            _repository.Register(client);
            var savedClient = await _repository.SearchByCURPAsync(client.CURP, AccountStatusEnum.Activo);

            bool result = _repository.DeleteById(savedClient.idCliente);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteById_ClientDoesNotExist_ReturnFalse()
        {

            bool result = _repository.DeleteById(NON_EXIST_CLIENT);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteById_IdIsNegativeOne_ReturnFalse()
        {
            int invalidId = -1;

            bool result = _repository.DeleteById(invalidId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task EditById_ClienteValid_RetornTrue()
        {
            const string NEW_CURP = "GARP900101HDFRLR19";
            const string NEW_RFC = "GARP900101MN9";
            var client = new Cliente
            {
                nombre = "Pedro",
                apellidoPaterno = "García",
                apellidoMaterno = "Hernández",
                telefono = "5555555555",
                codigoPostal = "54321",
                correo = "pedro@example.com",
                CURP = NEW_CURP,
                RFC = NEW_RFC,
                estado = AccountStatusEnum.Activo.ToString()
            };

            _repository.Register(client);
            var savedClient = await _repository.SearchByCURPAsync(client.CURP, AccountStatusEnum.Activo);

            var updatedClient = new Cliente
            {
                idCliente = savedClient.idCliente,
                nombre = "Pedro Editado",
                apellidoPaterno = "García",
                apellidoMaterno = "Hernández",
                telefono = "5555555555",
                codigoPostal = "54321",
                correo = "pedro.editado@example.com",
                CURP = NEW_CURP,
                RFC = NEW_RFC,
                estado = AccountStatusEnum.Activo.ToString()
            };

            bool result = _repository.Edit(updatedClient);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task EditById_CurpRFCNulls_RetornFalse()
        {
            var client = new Cliente
            {
                nombre = "Pedro",
                apellidoPaterno = "García",
                apellidoMaterno = "Hernández",
                telefono = "5555555555",
                codigoPostal = "54321",
                correo = "pedro@example.com",
                CURP = "GARP900101HDFRLR05",
                RFC = "GARP900101MN1",
                estado = AccountStatusEnum.Activo.ToString()
            };

            _repository.Register(client);
            var savedClient = await _repository.SearchByCURPAsync(client.CURP, AccountStatusEnum.Activo);

            var updatedClient = new Cliente
            {
                idCliente = savedClient.idCliente,
                nombre = "Pedro Editado",
                apellidoPaterno = "García",
                apellidoMaterno = "Hernández",
                telefono = "5555555555",
                codigoPostal = "54321",
                correo = "pedro.editado@example.com",
                CURP = "", 
                RFC = "",  
                estado = AccountStatusEnum.Activo.ToString()
            };

            bool result = _repository.Edit(updatedClient);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EditById_ClienteNoExist_RetornFalse()
        {
            var updatedClient = new Cliente
            {
                idCliente = NON_EXIST_CLIENT,
                nombre = "Pedro Editado",
                apellidoPaterno = "García",
                apellidoMaterno = "Hernández",
                telefono = "5555555555",
                codigoPostal = "54321",
                correo = "pedro.editado@example.com",
                CURP = "GARP900101HDFRLR05",
                RFC = "GARP900101MN1",
                estado = AccountStatusEnum.Activo.ToString()
            };

            bool result = _repository.Edit(updatedClient);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task SearchByIdAsync_ValidId_ReturnsClient()
        {
            var client = new Cliente
            {
                nombre = "Pedro",
                apellidoPaterno = "García",
                apellidoMaterno = "Hernández",
                telefono = "5555555555",
                codigoPostal = "54321",
                correo = "pedro@example.com",
                CURP = "GARP900101HDFRLR05",
                RFC = "GARP900101MN1",
                estado = AccountStatusEnum.Activo.ToString()
            };

            _repository.Register(client);

            var resultCurp = await _repository.SearchByCURPAsync(client.CURP, AccountStatusEnum.Activo);
            var result = await _repository.SearchByIdAsync(resultCurp.idCliente, AccountStatusEnum.Activo);

            Assert.IsNotNull(result);
            Assert.AreEqual(resultCurp.idCliente, result.idCliente);
        }

        [TestMethod]
        public async Task SearchByIdAsync_InvalidId_ReturnsDefaultClient()
        {
            var result = await _repository.SearchByIdAsync(-1, AccountStatusEnum.Activo);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.idCliente);
        }

        [TestMethod]
        public async Task SearchByIdAsync_ValidButNonExistentId_ReturnsDefaultClient()
        {
            var result = await _repository.SearchByIdAsync(99999, AccountStatusEnum.Activo);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.idCliente);
        }

        [TestMethod]
        public async Task SearchByCURPAsync_ValidCURP_ReturnsClient()
        {
            var client = new Cliente
            {
                nombre = "Pedro",
                apellidoPaterno = "García",
                apellidoMaterno = "Hernández",
                telefono = "5555555555",
                codigoPostal = "54321",
                correo = "pedro@example.com",
                CURP = "GARP900101HDFRLR05",
                RFC = "GARP900101MN1",
                estado = AccountStatusEnum.Activo.ToString()
            };

            _repository.Register(client);

            var result = await _repository.SearchByCURPAsync(client.CURP, AccountStatusEnum.Activo);

            Assert.IsNotNull(result);
            Assert.AreEqual(client.CURP, result.CURP);
        }

        [TestMethod]
        public async Task SearchByCURPAsync_NonExistentCURP_ReturnsDefaultClient()
        {
            var result = await _repository.SearchByCURPAsync("NONEXISTENTCURP123", AccountStatusEnum.Activo);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.idCliente);
        }

        [TestMethod]
        public async Task SearchByCURPAsync_InvalidCURP_ThrowsArgumentException()
        {
            var result = await _repository.SearchByCURPAsync("", AccountStatusEnum.Activo); 

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.idCliente);
        }

        [TestMethod]
        public async Task SearchByPagesAsync_ValidPage_ReturnsClients()
        {
            for (int i = 0; i < 10; i++)
            {
                _repository.Register (new Cliente
                {
                    nombre = $"Cliente {i}",
                    apellidoPaterno = "Apellido",
                    apellidoMaterno = "Apellido",
                    telefono = "1234567890",
                    codigoPostal = "12345",
                    correo = $"cliente{i}@example.com",
                    CURP = $"CURP{i}12345678",
                    RFC = $"RFC{i}12345678",
                    estado = AccountStatusEnum.Activo.ToString()
                });
            }

            var result = await _repository.SearchByPagesAsync(1, 3, AccountStatusEnum.Activo, 5);

            foreach (var actualClient in result)
            {
                Console.WriteLine(actualClient.nombre);
            }
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count >= 10);
        }

        [TestMethod]
        public async Task SearchByPagesAsync_PageTooFar_ReturnsEmptyList()
        {
            var result = await _repository.SearchByPagesAsync(99999, 1, AccountStatusEnum.Activo, 5);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task SearchByPagesAsync_ZeroValues_ReturnsEmptyList()
        {
            var result = await _repository.SearchByPagesAsync(0, 0, AccountStatusEnum.Activo, 0);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
