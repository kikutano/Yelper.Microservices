using EventBus.Events;
using EventBus.Interfaces;
using Identity.Application.Common.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;
using Xunit;

namespace Tests.Common.ApiFactories;

public abstract class ApiTestFixture<TProgram, TDbContext> : IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    public HttpClient ApiClient { get; private set; } = null!;

    private readonly MsSqlContainer _msSqlContainer = null!;
    private readonly RabbitMqContainer _rabbitMqContainer = null!;
    public readonly ApiFactory<TProgram, TDbContext> _apiFactory;

    public ApiTestFixture()
    {
        _msSqlContainer = new MsSqlBuilder().Build();
        _rabbitMqContainer = new RabbitMqBuilder().Build();
        _apiFactory = new ApiFactory<TProgram, TDbContext>(_msSqlContainer, _rabbitMqContainer);
    }

    public virtual async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        await _rabbitMqContainer.DisposeAsync().AsTask();
        await _apiFactory.DisposeAsync();
    }

    public virtual async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        await _rabbitMqContainer.StartAsync();
        await _apiFactory.InitializeAsync();
        ApiClient = _apiFactory.CreateClient();
    }

    public void Auth(Guid userId, string at)
    {
        var configuration = (IConfiguration)_apiFactory.Services.GetService(typeof(IConfiguration))!;

        var jwtToken = new JwtTokenAuthService()
             .GenerateToken(userId, at, configuration);

        ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
    }

    public void TriggerIntegrationEvent<T>(T evt) where T : IntegrationEvent
    {
        var eventBus = (IEventBus)_apiFactory.Services.GetService(typeof(IEventBus))!;
        eventBus.Publish(evt);
    }
}

