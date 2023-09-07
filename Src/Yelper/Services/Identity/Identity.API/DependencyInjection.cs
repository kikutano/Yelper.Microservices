using Identity.API.Common.Configurations;
using Identity.API.Common.Mapping;

namespace Identity.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddMappings();
        services.AddSwaggerConfiguration(configuration);

        services.AddCors(options =>
        {
            options.AddPolicy(
            "CorsPolicy",
             builder => builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
        });

        return services;
    }
}