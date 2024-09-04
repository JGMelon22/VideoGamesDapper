using VideoGamesDapper.Infrastructure.Repositories;
using VideoGamesDapper.Interfaces;

namespace VideoGamesDapper.Configuration;

public static class RepositoriesCollection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVideoGameRepository, VideoGameRepository>();
        return services;
    }
}
