namespace WristbandRadio.FileServer.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkImpl>()
            .AddTransient<IUsersRepository, UsersRepository>();

        return services;
    }
}
