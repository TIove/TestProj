using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestProj.Models;

namespace TestProj.Postgres;

public sealed class ApplicationContext : DbContext
{
    public DbSet<DbUser> Users { get; set; } = null!;

    public ApplicationContext()
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test_db;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load(typeof(DbUser).Assembly.FullName!));
    }
}