using Writer.API.Common.Configurations;

namespace Writer.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
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
