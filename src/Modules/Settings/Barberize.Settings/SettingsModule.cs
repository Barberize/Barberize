namespace Barberize.Settings;

public static class SettingsModule
{
    public static IServiceCollection AddSettingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    public static IApplicationBuilder UseSettingsModule(this IApplicationBuilder app)
    {
        return app;
    }
}
