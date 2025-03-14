﻿using System;
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
    [TestClass]
    public class Ayuda
    {
        private static MsSqlContainer _msSqlContainer;
        private static string connectionString;

        [ClassInitialize]
        public static async Task Setup(TestContext context)
        {
            // Construimos el contenedor usando MsSqlBuilder

            _msSqlContainer = new MsSqlBuilder()
                .WithPassword(Constants.CONTRASENIA_PRUEBA)
                // .WithImage("mcr.microsoft.com/mssql/server:2022-latest") // (Opcional) para especificar la versión de la imagen
                .Build();

            // Iniciamos el contenedor

            await _msSqlContainer.StartAsync();


            //  Ejecuta el script de inicialización para crear la base de datos y poblarla
            await SqlScriptExecutor.ExecuteScriptAsync(_msSqlContainer.GetConnectionString(), Constants.deletedbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(_msSqlContainer.GetConnectionString(), Constants.initdbScriptPath);

            var builder = new SqlConnectionStringBuilder(_msSqlContainer.GetConnectionString())
            {
                InitialCatalog = "AutoImperial"
            };
            string autoImperialConnectionString = builder.ToString();

            await SqlScriptExecutor.ExecuteScriptAsync(autoImperialConnectionString, Constants.initCatalogdbScriptPath);
            await SqlScriptExecutor.ExecuteScriptAsync(autoImperialConnectionString, Constants.initDataScriptPath);

        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            // Detenemos el contenedor cuando finalicen todos los tests de la clase
            await _msSqlContainer.StopAsync();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task Test_UsingEFCoreContext()
        {
            // Configuramos las opciones para el contexto usando la cadena del contenedor.
            var options = new DbContextOptionsBuilder<AutoImperialContext>()
                .UseSqlServer(_msSqlContainer.GetConnectionString())
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
        [TestMethod]
        public void TestMethod2()
        {
            var options = new DbContextOptionsBuilder<AutoImperialContext>()
                .UseSqlServer(_msSqlContainer.GetConnectionString())
                .Options;

            using (var context = new AutoImperialContext(options))
            {
                var entidad = context.Administradors.Where(admin => admin.nombreUsuario.Equals("a1", StringComparison.OrdinalIgnoreCase));
                Assert.IsNotNull(entidad);
            }
        }

    }
}
