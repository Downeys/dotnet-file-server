namespace WristbandRadio.FileServer.Common.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

        return services;
    }
}
