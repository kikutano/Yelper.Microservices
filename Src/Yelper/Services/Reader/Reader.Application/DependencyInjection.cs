using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Reader.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureMediator(services);
        ConfigurePersistence(services, configuration);

        return services;
    }

    private static void ConfigureMediator(IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
    }

    private static void ConfigurePersistence(IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = configuration["Redis:ConnectionString"];
                options.InstanceName = configuration["Redis:InstanceName"];
            });

        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            string connectionString = configuration["MongoDb:ConnectionString"];
            return new MongoClient(connectionString);
        });

        services.AddScoped<IMongoDatabase>(sp =>
        {
            string dbName = configuration["MongoDb:DatabaseName"];
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(dbName);
        });
    }
}

public class MongoDBSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
