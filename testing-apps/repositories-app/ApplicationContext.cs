namespace Repository_App;

using Microsoft.EntityFrameworkCore;
using Repository_App.Entities;
using Repository_App.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

public class ApplicationContext : DbContext
{
    private string? ConnectionString;
    public DbSet<User> Users => Set<User>();

    public DbSet<Test> Tests => Set<Test>();

    public ApplicationContext(string connectionString)
    {
        ConnectionString = connectionString;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (ConnectionString != null)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
    }
}