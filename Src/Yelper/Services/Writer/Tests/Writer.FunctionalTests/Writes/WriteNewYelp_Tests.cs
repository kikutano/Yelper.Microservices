using System.Net;
using Tests.Common.ApiFactories;
using Tests.Common.IntegrationEvents;
using Tests.Common.Networking;
using Writer.Application.Common.Persistence;
using Writer.Application.Writers.Queries;
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
        _fixture.TriggerIntegrationEvent(
            new UserCreatedIntegrationEvent(userId, "johnmclean", "Jonh McLean", ""));

        _fixture.Auth(userId, "johnmclain");

        var request = new CreateYelpRequest("an amazing new yelp!");

        var writerResponse = await RestApiCaller
            .PostAsync<RestApiResponse>(_fixture.ApiClient, "api/v1/writer", request);

        Assert.Equal(HttpStatusCode.OK, writerResponse.Response.StatusCode);

        var readerResponse = await RestApiCaller
            .GetAsync<ICollection<YelpItemCollectionResponse>>(
                _fixture.ApiClient, $"api/v1/reader/{userId}");

        Assert.Equal(HttpStatusCode.OK, readerResponse.Response.StatusCode);
        Assert.Equal(request.Text, readerResponse.Value.First().Text);
        Assert.True(readerResponse.Value.First().CreationAt > DateTime.UtcNow.AddMinutes(-2));
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
