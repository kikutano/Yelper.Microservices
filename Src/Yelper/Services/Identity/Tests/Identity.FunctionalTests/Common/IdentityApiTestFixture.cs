using Identity.Application.Auth.Commands;
using Identity.Application.Auth.Common;
using System.Net.Http.Headers;
using Testcontainers.MsSql;
using Tests.Common.Networking;

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

    public async Task Auth(string at, string accessCode)
    {
        var authRequest = new AuthRequestCommand(at, accessCode);

        var response = await RestApiCaller
            .PostAsync<AuthRequestResult>(ApiClient, $"api/v1/auth", authRequest);

        ApiClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", response.Value.Token);
    }
}
