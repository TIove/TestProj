using Microsoft.EntityFrameworkCore;
using TestProj.Postgres;

namespace TestProj.Web.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("SQLConnectionString"),
                b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
        });
    }
}