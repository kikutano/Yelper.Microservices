using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.Contracts.Followings;
using Identity.FunctionalTests.Common;
using System.Net;
using Tests.Common.Networking;

namespace Identity.FunctionalTests.Followings;

[Collection(nameof(ShareSameDatabaseInstance))]
public class FollowUser_Tests : IClassFixture<IdentityApiTestFixture>
{
    private readonly IdentityApiTestFixture Fixture;

    public FollowUser_Tests(IdentityApiTestFixture fixture)
    {
        Fixture = fixture;
    }

    [Fact]
    public async Task CreateNewUser_EnsureExistence()
    {
        //act
        var createJohnUserRequest = new CreateUserCommand("johnmclean", "John McLean");
        var createLoydUserRequest = new CreateUserCommand("loydchristmas", "Loyd Christmas");

        var createJohnUserResponse = await RestApiCaller
            .PostAsync<UserCreatedResult>(Fixture.ApiClient, "api/v1/user", createJohnUserRequest);
        var createLoydUserResponse = await RestApiCaller
            .PostAsync<UserCreatedResult>(Fixture.ApiClient, "api/v1/user", createLoydUserRequest);

        //arrange
        var followRequest = new FollowUserRequest(createLoydUserResponse.Value.User.UserId);

        var response = await RestApiCaller
            .PostAsync(Fixture.ApiClient, $"api/v1/following", followRequest);

        //assert
        Assert.Equal(HttpStatusCode.OK, response.Response.StatusCode);
    }
}
