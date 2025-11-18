namespace WristbandRadio.FileServer.Catalogue.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkImpl>()
            .AddTransient<ITrackRepository, TrackRepository>()
            .AddTransient<IArtistRepository, ArtistRepository>()
            .AddTransient<IAlbumRepository, AlbumRepository>()
            .AddTransient<ISongRepository, SongRepository>();

        return services;
    }
}
