using Identity.Application.Auth.Commands;
using Identity.Application.Auth.Common;
using Identity.Application.Common.Persistence;
using System.Net.Http.Headers;
using Tests.Common.ApiFactories;
using Tests.Common.Networking;

namespace Identity.FunctionalTests.Common;

public class IdentityApiTestFixture : ApiTestFixture<Program, IdentityDbContext>
{
    public async Task Auth(string at, string accessCode)
    {
        var authRequest = new AuthRequestCommand(at, accessCode);

        var response = await RestApiCaller
            .PostAsync<AuthRequestResult>(ApiClient, $"api/v1/auth", authRequest);

        ApiClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", response.Value.Token);
    }
}
