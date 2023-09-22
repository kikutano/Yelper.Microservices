using Identity.Application.Followers.Common;
using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.Contracts.Followings;
using Identity.FunctionalTests.Common;
using System.Net;
using Tests.Common.Networking;

namespace Identity.FunctionalTests.Followers;

[Collection(nameof(ShareSameDatabaseInstance))]
public class GetUserFollowers_Tests : IClassFixture<IdentityApiTestFixture>
{
    private readonly IdentityApiTestFixture _fixture;

    public GetUserFollowers_Tests(IdentityApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetUserFollowers_EnsureCorrectness()
    {
        //act
        var createJohnUserRequest = new CreateUserCommand("johnmclean", "John McLean");
        var createLoydUserRequest = new CreateUserCommand("loydchristmas", "Loyd Christmas");

        var createJohnUserResponse = await RestApiCaller
            .PostAsync<UserCreatedResult>(_fixture.ApiClient, "api/v1/user", createJohnUserRequest);
        var createLoydUserResponse = await RestApiCaller
            .PostAsync<UserCreatedResult>(_fixture.ApiClient, "api/v1/user", createLoydUserRequest);

        await _fixture.Auth("johnmclean", createJohnUserResponse.Value.AccessCode);

        var followRequest = new FollowUserRequest(createLoydUserResponse.Value.User.UserId);

        await RestApiCaller.PostAsync(_fixture.ApiClient, $"api/v1/following", followRequest);

        //arrange
        var followersResponse = await RestApiCaller
            .GetAsync<List<FollowerResult>>(_fixture.ApiClient, $"api/v1/followers/loydchristmas");

        //assert
        Assert.Equal(HttpStatusCode.OK, followersResponse.Response.StatusCode);
        Assert.Single(followersResponse.Value);
        Assert.Equal(createJohnUserResponse.Value.User.At, followersResponse.Value[0].At);
        Assert.Equal(createJohnUserResponse.Value.User.Name, followersResponse.Value[0].Name);
    }
}

