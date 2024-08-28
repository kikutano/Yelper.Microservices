﻿using Identity.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

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
        services.AddDbContext<IdentityDbContext>(
            options =>
            {
                string connectionString = configuration["ConnectionStrings:Database"]!;

                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

        ApplyMigration(services, configuration);
    }

    private static void ApplyMigration(IServiceCollection services, IConfiguration configuration)
    {
        bool applyMigration = false;
        bool.TryParse(configuration["Database:ApplyMigration"]!, out applyMigration);
        if (applyMigration)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var context = serviceProvider.GetRequiredService<IdentityDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
