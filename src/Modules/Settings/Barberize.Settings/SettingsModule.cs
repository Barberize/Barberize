using Barberize.Settings.Infrastructure.Persistence;
using Barberize.Shared.Extensions;
using Barberize.Shared.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Barberize.Settings;

public static class SettingsModule
{
    public static IServiceCollection AddSettingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<SettingsDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IApplicationBuilder UseSettingsModule(this IApplicationBuilder app)
    {
        app.UseMigration<SettingsDbContext>();
        return app;
    }
}
