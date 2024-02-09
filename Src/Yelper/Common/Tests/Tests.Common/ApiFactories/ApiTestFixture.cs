using Microsoft.EntityFrameworkCore;
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
    private readonly ApiFactory<TProgram, TDbContext> _apiFactory;

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

    //public async Task Auth(string at, string accessCode)
    //{
    //    var authRequest = new AuthRequestCommand(at, accessCode);

    //    var response = await RestApiCaller
    //        .PostAsync<AuthRequestResult>(ApiClient, $"api/v1/auth", authRequest);

    //    ApiClient.DefaultRequestHeaders.Authorization =
    //        new AuthenticationHeaderValue("Bearer", response.Value.Token);
    //}
}
