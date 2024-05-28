using EventBus.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQEventBus;
using Testcontainers.RabbitMq;
using Testcontainers.Redis;

namespace Reader.FunctionalTests.Common;

public class ReaderApiFactory<TProgram> : WebApplicationFactory<Program>
{
    private readonly RedisContainer _redisContainer;
    private readonly RabbitMqContainer _rabbitMqContainer = null!;

    public ReaderApiFactory(RedisContainer redisContainer, RabbitMqContainer rabbitMqContainer)
    {
        _redisContainer = redisContainer;
        _rabbitMqContainer = rabbitMqContainer;
    }

    public async Task InitializeAsync()
    {

    }

    public async Task DisposeAsync()
    {

    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ConfigureEventBusUsingRabbitMqOnDockerContainer(services);
            ConfigureRedisOnDockerContainer(services);
        });

        base.ConfigureWebHost(builder);
    }

    private void ConfigureRedisOnDockerContainer(IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = _redisContainer.GetConnectionString();
                options.InstanceName = "reader_redis_testcontainers";
            });
    }

    private void ConfigureEventBusUsingRabbitMqOnDockerContainer(IServiceCollection services)
    {
        var connectionString = _rabbitMqContainer.GetConnectionString();
        var rabbitMq = new EventBusRabbitMQ(new InMemoryEventBusSubscriptionsManager(), connectionString);

        services.RemoveAll<IEventBus>();
        services.AddSingleton<IEventBus>(rabbitMq);
    }
}
