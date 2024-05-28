using EventBus.Events;
using EventBus.Interfaces;
using Testcontainers.RabbitMq;
using Testcontainers.Redis;

namespace Reader.FunctionalTests.Common;

public class ReaderApiTestFixture : IAsyncLifetime
{
    public HttpClient ApiClient { get; private set; } = null!;

    private readonly RabbitMqContainer _rabbitMqContainer = null!;
    private readonly RedisContainer _redisContainer = null!;
    private readonly ReaderApiFactory<Program> _apiFactory;

    public ReaderApiTestFixture()
    {
        _rabbitMqContainer = new RabbitMqBuilder().Build();
        _redisContainer = new RedisBuilder().Build();
        _apiFactory = new ReaderApiFactory<Program>(_redisContainer, _rabbitMqContainer);
    }

    public async Task DisposeAsync()
    {
        await _redisContainer.DisposeAsync();
        await _rabbitMqContainer.DisposeAsync();
        await _apiFactory.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        await _redisContainer.StartAsync();
        await _rabbitMqContainer.StartAsync();
        ApiClient = _apiFactory.CreateClient();
    }

    public void TriggerIntegrationEvent<T>(T evt) where T : IntegrationEvent
    {
        var eventBus = (IEventBus)_apiFactory.Services.GetService(typeof(IEventBus))!;
        eventBus.Publish(evt);
    }
}
