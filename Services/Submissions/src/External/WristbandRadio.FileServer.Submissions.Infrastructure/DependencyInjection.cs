namespace WristbandRadio.FileServer.Submissions.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkImpl>()
            .AddTransient<IMusicSubmissionBlobService, MusicSubmissionBlobService>();

        return services;
    }
}
