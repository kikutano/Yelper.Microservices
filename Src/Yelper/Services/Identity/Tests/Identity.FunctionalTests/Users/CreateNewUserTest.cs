using Identity.Contracts.Users;
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
        var request = new CreateUserRequest("johnmclean", "John McLean");

        var response = await RestApiCaller
            .PostAsync<UserCreatedDto>(Fixture.ApiClient, "api/v1/user", request);

        //assert
        Assert.Equal(HttpStatusCode.OK, response.Response.StatusCode);
    }

    [Fact]
    public async Task CreateNewUser_EnsureExistence2()
    {
        //arrange
        var request = new CreateUserRequest("johnmclean", "John McLean");

        var response = await RestApiCaller
            .PostAsync<UserCreatedDto>(Fixture.ApiClient, "api/v1/user", request);

        //assert
        Assert.Equal(HttpStatusCode.OK, response.Response.StatusCode);
    }
}