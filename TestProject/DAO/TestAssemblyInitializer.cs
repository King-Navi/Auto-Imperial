using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using TestProject.DAO.Utilities;

namespace TestProject.DAO
{
    [TestClass]
    public static class TestAssemblyInitializer
    {
        private static MsSqlContainer _msSqlContainer;
        public static string ForcedConnectionString { get; private set; }
        public static MsSqlContainer MsSqlContainer { get => _msSqlContainer; private set => _msSqlContainer = value; }

        [AssemblyInitialize]
        public static async Task AssemblySetup(TestContext context)
        {
            Console.WriteLine("Initializing AssemblySetup...");
            MsSqlContainer = new MsSqlBuilder()
                .WithPassword(ConstantsTestDAO.CONTRASENIA_PRUEBA)
                .Build();

            await MsSqlContainer.StartAsync();
            Console.WriteLine("Container started.");


            await WaitUntilDataBase(MsSqlContainer.GetConnectionString());
            Console.WriteLine("Database connection established.");

            await SqlScriptExecutor.ExecuteScriptAsync(MsSqlContainer.GetConnectionString(), ConstantsTestDAO.deletedbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(MsSqlContainer.GetConnectionString(), ConstantsTestDAO.initdbScriptPath);
           // await SqlScriptExecutor.ExecuteScriptAsync(MsSqlContainer.GetConnectionString(), Constants.createUserbScriptPath);

            var builder = new SqlConnectionStringBuilder(MsSqlContainer.GetConnectionString())
            {
                InitialCatalog = "AutoImperial",
                UserID = "sa",
                Password = ConstantsTestDAO.CONTRASENIA_PRUEBA
            };
            ForcedConnectionString = builder.ToString();

            await SqlScriptExecutor.ExecuteScriptAsync(ForcedConnectionString, ConstantsTestDAO.initCatalogdbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(ForcedConnectionString, ConstantsTestDAO.initDataScriptPath);

            await VerifyTableExists();
            Console.WriteLine("AssemblySetup completed.");
        }

        [AssemblyCleanup]
        public static async Task AssemblyCleanup()
        {
            if (MsSqlContainer != null)
            {
                await MsSqlContainer.StopAsync();
            }
        }

        private static async Task WaitUntilDataBase(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    await connection.OpenAsync();
                    return;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }
            throw new Exception("Couldn´t Connect with data base : TestAssemblyInitializer.cs : WaitUntilDataBase() ");
        }
        private static async Task VerifyTableExists()
        {
            try
            {
                using var connection = new SqlConnection(ForcedConnectionString);
                await connection.OpenAsync();

                string query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cliente'";

                using var command = new SqlCommand(query, connection);
                int tableCount = (int)await command.ExecuteScalarAsync();

                if (tableCount == 0)
                {
                    throw new Exception("ERROR: La tabla 'Cliente' no fue encontrada en la base de datos.");
                }

                Console.WriteLine("Tabla 'Cliente' encontrada en la base de datos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verificando la tabla 'Clientes': {ex.Message}");
                throw;
            }
        }
    }
}
