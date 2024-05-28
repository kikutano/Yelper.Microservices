using Reader.API.Common.Configurations;
using Reader.API.Common.Mapping;

namespace Reader.API;

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
