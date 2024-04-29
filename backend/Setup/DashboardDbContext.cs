using backend.Configuration;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Setup;

public class DashboardDbContext(Config config) : DbContext
{
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(config.connectionString, config.MysqlServerVersion)
            .LogTo(Console.WriteLine, LogLevel.Warning)
            .EnableDetailedErrors(); // TODO check if app is in dev mode before enabling this
    }
}