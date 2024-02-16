using System.Net;
using Tests.Common.ApiFactories;
using Tests.Common.IntegrationEvents;
using Tests.Common.Networking;
using Writer.Application.Common.Persistence;
using Writer.Contracts.Writes;
using Writer.FunctionalTests.Common;

namespace Writer.FunctionalTests.Writes;

[Collection(nameof(ShareSameDatabaseInstance<Program, WriterDbContext>))]
public class WriteNewYelp_Tests : IClassFixture<WriterApiTestFixture>
{
    private readonly WriterApiTestFixture _fixture;

    public WriteNewYelp_Tests(WriterApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task WriteNewYelp_EnsureCorrectness()
    {
        var userId = Guid.NewGuid();
        _fixture.LauchIntegrationEvent(
            new UserCreatedIntegrationEvent(userId, "johnmclean", "Jonh McLean", ""));

        _fixture.Auth(userId, "johnmclain");

        var request = new CreateYelpRequest("an amazing new yelp!");

        var response = await RestApiCaller
            .PostAsync<RestApiResponse>(_fixture.ApiClient, "api/v1/writer", request);

        Assert.Equal(HttpStatusCode.OK, response.Response.StatusCode);
    }

    [Fact]
    public async Task WriteNewYelp_WithNotExistingUser_MustReturnNotFound()
    {
        _fixture.Auth(Guid.NewGuid(), "johnmclain");

        var request = new CreateYelpRequest("an amazing new yelp!");

        var response = await RestApiCaller
            .PostAsync<RestApiResponse>(_fixture.ApiClient, "api/v1/writer", request);

        Assert.Equal(HttpStatusCode.NotFound, response.Response.StatusCode);
    }
}
