using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.FunctionalTests.Common;
using System.Net;
using Tests.Common.Networking;

namespace Identity.FunctionalTests.Users;

[Collection(nameof(ShareSameDatabaseInstance))]
public class CreateNewUserTest : IClassFixture<IdentityApiTestFixture>
{
    private readonly IdentityApiTestFixture Fixture;

    public CreateNewUserTest(IdentityApiTestFixture fixture)
    {
        Fixture = fixture;
    }

    [Fact]
    public async Task CreateNewUser_EnsureExistence()
    {
        //act
        var request = new CreateUserCommand("johnmclean", "John McLean");

        //arrange
        var creationResponse = await RestApiCaller
            .PostAsync<UserCreatedResult>(Fixture.ApiClient, "api/v1/user", request);

        var retrieveResponse = await RestApiCaller
            .GetAsync<UserResult>(
                Fixture.ApiClient,
                $"api/v1/user/{creationResponse.Value.User.Identifier}");

        //assert
        Assert.Equal(HttpStatusCode.OK, creationResponse.Response.StatusCode);
        Assert.Equal(request.Name, creationResponse.Value.User.Name);
        Assert.Equal(request.Identifier, creationResponse.Value.User.Identifier);

        Assert.Equal(creationResponse.Value.User.Identifier, retrieveResponse.Value.Identifier);
        Assert.Equal(creationResponse.Value.User.Name, retrieveResponse.Value.Name);
    }
}