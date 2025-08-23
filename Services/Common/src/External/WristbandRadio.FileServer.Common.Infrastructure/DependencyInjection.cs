namespace WristbandRadio.FileServer.Common.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
        services.AddScoped<DapperDataContext>();
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        return services;
    }
}
