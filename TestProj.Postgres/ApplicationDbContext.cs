using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestProj.Models;

namespace TestProj.Postgres;

public sealed class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<DbUser> Users { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load(typeof(DbUser).Assembly.FullName!));
    }
}