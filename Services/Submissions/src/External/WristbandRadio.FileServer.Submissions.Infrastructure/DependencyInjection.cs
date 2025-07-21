using Microsoft.Extensions.DependencyInjection;
using WristbandRadio.FileServer.Submissions.Infrastructure.UnitOfWork;

namespace WristbandRadio.FileServer.Submissions.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<MusicSubmissionContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWorkImpl>();
        services.AddScoped<IMusicSubmissionRepository, MusicSubmissionRepository>();
        return services;
    }
}
