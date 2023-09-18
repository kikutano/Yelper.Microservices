using Testcontainers.MsSql;

namespace Identity.FunctionalTests.Common;

public class IdentityApiTestFixture : IAsyncLifetime
{
    public HttpClient ApiClient { get; private set; } = null!;

    private readonly MsSqlContainer _msSqlContainer = null!;
    private readonly IdentityApiFactory<Program> _identityApiFactory;

    public IdentityApiTestFixture()
    {
        _msSqlContainer = new MsSqlBuilder().Build();
        _identityApiFactory = new IdentityApiFactory<Program>(_msSqlContainer);
    }

    public async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        await _identityApiFactory.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        await _identityApiFactory.InitializeAsync();
        ApiClient = _identityApiFactory.CreateClient();
    }
}
