namespace Repository_App;

using Microsoft.EntityFrameworkCore;
using Repository_App.Entities;
using Repository_App.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

public class ApplicationContext : DbContext
{
    private readonly string PathToConnectionFile;
    public DbSet<User> Users => Set<User>();

    public DbSet<Test> Tests => Set<Test>();

    public ApplicationContext(string pathToConnectionFile)
    {
        PathToConnectionFile = pathToConnectionFile;

        Database.EnsureCreated();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(new ConfigurationBuilder()
                                        .SetBasePath(PathToConnectionFile)
                                        .AddJsonFile("appsetings.json")
                                        .Build()
                                        .GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
    }
}