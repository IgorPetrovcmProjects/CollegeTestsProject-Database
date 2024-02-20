namespace Repository_App;

using Microsoft.EntityFrameworkCore;
using Repository_App.Entities;

public class ApplicationContext : DbContext
{
    private readonly string? ConnectionString; 

    public DbSet<User> Users => Set<User>();

    public DbSet<Test> Tests => Set<Test>();

    public ApplicationContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
    }


}