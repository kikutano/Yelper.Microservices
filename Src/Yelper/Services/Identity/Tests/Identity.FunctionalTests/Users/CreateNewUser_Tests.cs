using Identity.Application.Common.Persistence;
using Identity.Application.Users.Commands;
using Identity.Application.Users.Common;
using Identity.FunctionalTests.Common;
using System.Net;
using Tests.Common.ApiFactories;
using Tests.Common.Networking;

namespace Identity.FunctionalTests.Users;

[Collection(nameof(ShareSameDatabaseInstance<Program, IdentityDbContext>))]
public class CreateNewUser_Tests : IClassFixture<IdentityApiTestFixture>
{
	private readonly IdentityApiTestFixture Fixture;

	public CreateNewUser_Tests(IdentityApiTestFixture fixture)
	{
		Fixture = fixture;
	}

	[Fact(Skip = "yes")]
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
				$"api/v1/user/{creationResponse.Value.User.At}");

		//assert
		Assert.Equal(HttpStatusCode.OK, creationResponse.Response.StatusCode);
		Assert.Equal(request.Name, creationResponse.Value.User.Name);
		Assert.Equal(request.At, creationResponse.Value.User.At);

		Assert.Equal(creationResponse.Value.User.At, retrieveResponse.Value.At);
		Assert.Equal(creationResponse.Value.User.Name, retrieveResponse.Value.Name);
	}
}