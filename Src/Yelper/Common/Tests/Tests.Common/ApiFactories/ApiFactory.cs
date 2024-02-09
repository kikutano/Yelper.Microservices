using EventBus.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQEventBus;
using System.Data.Common;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;

namespace Tests.Common.ApiFactories;

public class ApiFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>
    where TProgram : class where TDbContext : DbContext
{
    private readonly MsSqlContainer _msSqlContainer = null!;
    private readonly RabbitMqContainer _rabbitMqContainer = null!;

    public ApiFactory(MsSqlContainer msSqlContainer, RabbitMqContainer rabbitMqContainer)
    {
        _msSqlContainer = msSqlContainer;
        _rabbitMqContainer = rabbitMqContainer;
    }

    public async Task InitializeAsync()
    {
        await PerformDbInitialization();
    }

    public async Task DisposeAsync()
    {
        await Task.CompletedTask;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ConfigureDatabaseUsingSqlServerOnDockerContainer(services);
            ConfigureEventBusUsingRabbitMqOnDockerContainer(services);
        });

        base.ConfigureWebHost(builder);
    }

    private void ConfigureDatabaseUsingSqlServerOnDockerContainer(IServiceCollection services)
    {
        services.RemoveAll<DbContextOptions<TDbContext>>();
        services.RemoveAll<DbConnection>();

        services.AddDbContext<TDbContext>(options =>
        {
            options.UseSqlServer(_msSqlContainer.GetConnectionString());
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }

    private void ConfigureEventBusUsingRabbitMqOnDockerContainer(IServiceCollection services)
    {
        var connectionString = _rabbitMqContainer.GetConnectionString();
        var rabbitMq = new EventBusRabbitMQ(new InMemoryEventBusSubscriptionsManager(), connectionString);

        services.RemoveAll<IEventBus>();
        services.AddSingleton<IEventBus>(rabbitMq);
    }

    private async Task PerformDbInitialization()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
