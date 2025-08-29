namespace WristbandRadio.FileServer.Catalogue.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkImpl>()
            .AddTransient<ITrackRepository, TrackRepository>();

        return services;
    }
}
