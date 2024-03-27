using Identity.Application.Common.Persistence;
using Identity.Application.Followings.Common;
using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.Contracts.Followings;
using Identity.FunctionalTests.Common;
using System.Net;
using Tests.Common.ApiFactories;
using Tests.Common.Networking;

namespace Identity.FunctionalTests.Followings;

[Collection(nameof(ShareSameDatabaseInstance<Program, IdentityDbContext>))]
public class UnfollowUser_Tests : IClassFixture<IdentityApiTestFixture>
{
	private readonly IdentityApiTestFixture _fixture;

	public UnfollowUser_Tests(IdentityApiTestFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact(Skip = "yes")]
	public async Task UnfollowUser_EnsureCorrectness()
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

		await RestApiCaller.PostAsync(_fixture.ApiClient, $"api/v1/following", followRequest);

		var unfollowRequestResponse = await RestApiCaller
			.DeleteAsync(_fixture.ApiClient, $"api/v1/following/{createLoydUserResponse.Value.User.UserId}");

		var followingsGetResponse = await RestApiCaller
			.GetAsync<List<UserFollowingResult>>(_fixture.ApiClient, $"api/v1/following/johnmclean");

		//assert
		Assert.Equal(HttpStatusCode.OK, unfollowRequestResponse.Response.StatusCode);
		Assert.Empty(followingsGetResponse.Value);
	}
}
