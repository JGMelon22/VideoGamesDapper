using FluentValidation;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Infrastructure.Validators;

namespace VideoGamesDapper.Configuration;

public static class ValidatorsCollection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<VideoGameInput>, VideoGameValidator>();
        return services;
    }
}
