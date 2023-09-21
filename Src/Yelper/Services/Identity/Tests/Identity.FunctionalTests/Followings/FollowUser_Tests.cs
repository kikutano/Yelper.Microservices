using Identity.Application.Followings.Common;
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
    private readonly IdentityApiTestFixture _fixture;

    public FollowUser_Tests(IdentityApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task FollowUser_EnsureCorrectness()
    {
        //act
        var createJohnUserRequest = new CreateUserCommand("johnmclean", "John McLean");
        var createLoydUserRequest = new CreateUserCommand("loydchristmas", "Loyd Christmas");

        var createJohnUserResponse = await RestApiCaller
            .PostAsync<UserCreatedResult>(_fixture.ApiClient, "api/v1/user", createJohnUserRequest);
        var createLoydUserResponse = await RestApiCaller
            .PostAsync<UserCreatedResult>(_fixture.ApiClient, "api/v1/user", createLoydUserRequest);

        await _fixture.Auth("johnmclean", createJohnUserResponse.Value.AccessCode);

        //arrange
        var followRequest = new FollowUserRequest(createLoydUserResponse.Value.User.UserId);

        var followRequestResponse = await RestApiCaller
            .PostAsync(_fixture.ApiClient, $"api/v1/following", followRequest);

        var followingsGetResponse = await RestApiCaller
            .GetAsync<List<UserFollowingResult>>(_fixture.ApiClient, $"api/v1/following/johnmclean");

        //assert
        Assert.Equal(HttpStatusCode.OK, followRequestResponse.Response.StatusCode);
        Assert.Single(followingsGetResponse.Value);
        Assert.Equal(createLoydUserResponse.Value.User.At, followingsGetResponse.Value[0].At);
        Assert.Equal(createLoydUserResponse.Value.User.Name, followingsGetResponse.Value[0].Name);
    }
}
