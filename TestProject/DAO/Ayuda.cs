using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DAO.Utilities;
using Testcontainers.MsSql;
using Microsoft.Data.SqlClient;
using Microsoft.CodeCoverage.Core.Reports.Coverage;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TestProject.DAO
{
    //FIXME: Borrar solo cunado todos los miembros del equipo hayan entendido el funcionamiento
    public class Ayuda
    {
        private static MsSqlContainer _msSqlContainer;
        private static string forcedConnectionString;

        public static async Task Setup(TestContext context)
        {

            _msSqlContainer = new MsSqlBuilder()
                .WithPassword(ConstantsTestDAO.CONTRASENIA_PRUEBA)
                // .WithImage("mcr.microsoft.com/mssql/server:2022-latest") // (Opcional) para especificar la versión de la imagen
                .Build();
            // Iniciamos el contenedor

            await _msSqlContainer.StartAsync();

            await WaitUntilDataBase(_msSqlContainer.GetConnectionString());

            
            await SqlScriptExecutor.ExecuteScriptAsync(_msSqlContainer.GetConnectionString(), ConstantsTestDAO.deletedbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(_msSqlContainer.GetConnectionString(), ConstantsTestDAO.initdbScriptPath);
            //await SqlScriptExecutor.ExecuteScriptAsync(_msSqlContainer.GetConnectionString(), Constants.createUserbScriptPath);

            var builder = new SqlConnectionStringBuilder(_msSqlContainer.GetConnectionString())
            {
                InitialCatalog = "AutoImperial",
                UserID = "sa",
                Password = ConstantsTestDAO.CONTRASENIA_PRUEBA
            };
            forcedConnectionString = builder.ToString();

            await SqlScriptExecutor.ExecuteScriptAsync(forcedConnectionString, ConstantsTestDAO.initCatalogdbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(forcedConnectionString, ConstantsTestDAO.initDataScriptPath);

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
            throw new Exception("No se pudo conectar a SQL Server después de 10 intentos.");
        }


        public static async Task Cleanup()
        {
            await _msSqlContainer.StopAsync();
        }

        public async Task Test_UsingEFCoreContext()
        {
            var builder = new SqlConnectionStringBuilder(_msSqlContainer.GetConnectionString())
            {
                InitialCatalog = "AutoImperial",
                UserID = "sa",
                Password = ConstantsTestDAO.CONTRASENIA_PRUEBA
            };
            forcedConnectionString = builder.ToString();
            Console.WriteLine(forcedConnectionString);
            Console.WriteLine(_msSqlContainer.GetConnectionString());
            // Configuramos las opciones para el contexto usando la cadena del contenedor.
            var options = new DbContextOptionsBuilder<AutoImperialContext>()
                .UseSqlServer(forcedConnectionString)
                .Options;
            // 1. Crear el contexto y asegurarnos de que la base de datos esté creada.
            using (var context = new AutoImperialContext(options))
            {
                // Puedes usar EnsureCreated() para crear la base de datos con el esquema actual
                await context.Database.EnsureCreatedAsync();

                // (Opcional) Si usas migraciones, puedes aplicar: 
                // await context.Database.MigrateAsync();

                // 2. (Opcional) Sembrar datos iniciales si es necesario
                //context.SomeEntity.Add(new SomeEntity { Nombre = "Valor de Prueba" });
                await context.SaveChangesAsync();
            }

            // 3. Realizar operaciones de lectura/consulta y validar resultados.
            using (var context = new AutoImperialContext(options))
            {
                var entidad = await context.Clientes.FirstOrDefaultAsync();
                Assert.IsNotNull(entidad);
            }
        }

        public void TestMethod2()
        {
            var options = new DbContextOptionsBuilder<AutoImperialContext>()
                .UseSqlServer(_msSqlContainer.GetConnectionString())
                .Options;

            using (var context = new AutoImperialContext(options))
            {
                var entidad = context.Administradores.Where(admin => admin.nombreUsuario.Equals("a1", StringComparison.OrdinalIgnoreCase));
                Assert.IsNotNull(entidad);
            }
        }

    }
}
