using Identity.Application.Auth.Commands;
using Identity.Application.Auth.Common;
using Identity.Application.Common.Persistence;
using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.FunctionalTests.Common;
using System.Net;
using System.Net.Http.Headers;
using Tests.Common.ApiFactories;
using Tests.Common.Networking;

namespace Identity.FunctionalTests.Auth;

[Collection(nameof(ShareSameDatabaseInstance<Program, IdentityDbContext>))]
public class Auth_Tests : IClassFixture<IdentityApiTestFixture>
{
	private readonly IdentityApiTestFixture _fixture;

	public Auth_Tests(IdentityApiTestFixture fixture)
	{
		_fixture = fixture;
	}

	[Fact(Skip = "yes")]
	public async Task PerformAuth_EnsureSuccess()
	{
		//act
		var createJohnUserRequest = new CreateUserCommand("johnmclean", "John McLean");

		var createJohnUserResponse = await RestApiCaller
			.PostAsync<UserCreatedResult>(_fixture.ApiClient, "api/v1/user", createJohnUserRequest);

		//arrange
		var authRequest = new AuthRequestCommand("johnmclean", createJohnUserResponse.Value.AccessCode);

		var response = await RestApiCaller
			.PostAsync<AuthRequestResult>(_fixture.ApiClient, $"api/v1/auth", authRequest);

		_fixture.ApiClient.DefaultRequestHeaders.Authorization =
			new AuthenticationHeaderValue("Bearer", response.Value.Token);

		var imAuthorizedResponse = await RestApiCaller
			.GetAsync(_fixture.ApiClient, $"api/v1/auth/im_authorized");

		//assert
		Assert.Equal(HttpStatusCode.OK, response.Response.StatusCode);
		Assert.NotNull(response.Value.Token);
		Assert.Equal(HttpStatusCode.OK, imAuthorizedResponse.Response.StatusCode);
	}
}
