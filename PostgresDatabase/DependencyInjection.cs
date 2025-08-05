namespace WristbandRadio.FileServer.PostgresDatabase;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddSingleton(_ => new DatabaseInitializer(connectionString));
        return services;
    }
}
