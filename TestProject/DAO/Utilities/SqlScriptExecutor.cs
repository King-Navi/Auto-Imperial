using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace TestProject.DAO.Utilities
{
    internal class SqlScriptExecutor
    {
        public static async Task ExecuteScriptAsync(string connectionString, string scriptFilePath)
        {
            var script = await File.ReadAllTextAsync(scriptFilePath);

            var batches = script.Split(new[] { "\r\nGO\r\n", "\nGO\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (batches.Length == 0)
                return;

            for (int i = 0; i < batches.Length; i++)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(batches[i], connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

    }
}
