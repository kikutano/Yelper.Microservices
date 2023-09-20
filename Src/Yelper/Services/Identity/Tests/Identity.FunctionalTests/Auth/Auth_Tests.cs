using Identity.Application.Auth.Commands;
using Identity.Application.Auth.Common;
using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.FunctionalTests.Common;
using System.Net;
using Tests.Common.Networking;

namespace Identity.FunctionalTests.Auth;

[Collection(nameof(ShareSameDatabaseInstance))]
public class Auth_Tests : IClassFixture<IdentityApiTestFixture>
{
    private readonly IdentityApiTestFixture Fixture;

    public Auth_Tests(IdentityApiTestFixture fixture)
    {
        Fixture = fixture;
    }

    [Fact]
    public async Task PerformAuth_EnsureSuccess()
    {
        //act
        var createJohnUserRequest = new CreateUserCommand("johnmclean", "John McLean");

        var createJohnUserResponse = await RestApiCaller
            .PostAsync<UserCreatedResult>(Fixture.ApiClient, "api/v1/user", createJohnUserRequest);

        //arrange
        var authRequest = new AuthRequestCommand("johnmclean", createJohnUserResponse.Value.AccessCode);

        var response = await RestApiCaller
            .PostAsync<AuthRequestResult>(Fixture.ApiClient, $"api/v1/auth", authRequest);

        //assert
        Assert.Equal(HttpStatusCode.OK, response.Response.StatusCode);
        Assert.NotNull(response.Value.Token);
    }
}
