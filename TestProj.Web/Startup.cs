using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using TestProj.Domain;
using TestProj.Domain.Interfaces;
using TestProj.Postgres;
using TestProj.Web.Infrastructure.Extensions;
using TestProj.Web.Infrastructure.Middlewares;

namespace TestProj.Web;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;
    
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddMvc();

        services.AddInfrastructureDbContext(Configuration);

        services.AddHttpContextAccessor();

        services.AddMemoryCache();

        services.AddScoped<IUserService, UserService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }

        app.UseHttpsRedirection();

        UpdateDatabase(app);
        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
    
    private void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();

        // Must fail here if it is null
        context!.Database.Migrate();
    }
}