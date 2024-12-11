using System.Reflection;
using Microsoft.OpenApi.Models;
using TestProj.Web.Infrastructure.StartupFilters;
using TestProj.Web.Infrastructure.Swagger;

namespace TestProj.Web.Infrastructure.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
    {
        return builder.AddSwagger();
    }

    private static IHostBuilder AddSwagger(this IHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                        { Title = $"{Assembly.GetExecutingAssembly().GetName().Name}", Version = "v1" });

                options.CustomSchemaIds(x => x.FullName);

                options.EnableAnnotations();

                options.OperationFilter<HeaderOperationFilter>();
            });
        });
    }
}