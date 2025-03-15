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
                .WithPassword(Constants.CONTRASENIA_PRUEBA)
                .Build();

            await MsSqlContainer.StartAsync();
            Console.WriteLine("Container started.");


            await WaitUntilDataBase(MsSqlContainer.GetConnectionString());
            Console.WriteLine("Database connection established.");

            await SqlScriptExecutor.ExecuteScriptAsync(MsSqlContainer.GetConnectionString(), Constants.deletedbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(MsSqlContainer.GetConnectionString(), Constants.initdbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(MsSqlContainer.GetConnectionString(), Constants.createUserbScriptPath);

            var builder = new SqlConnectionStringBuilder(MsSqlContainer.GetConnectionString())
            {
                InitialCatalog = "AutoImperial",
                UserID = "sa",
                Password = Constants.CONTRASENIA_PRUEBA
            };
            ForcedConnectionString = builder.ToString();

            await SqlScriptExecutor.ExecuteScriptAsync(ForcedConnectionString, Constants.initCatalogdbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(ForcedConnectionString, Constants.initDataScriptPath);
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
    }
}
