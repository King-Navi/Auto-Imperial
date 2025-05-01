using Microsoft.EntityFrameworkCore;


namespace AutoImperialDAO.Models;

public partial class AutoImperialContext : DbContext
{
    private readonly string _connectionString;
    public AutoImperialContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}

