using Reader.Application.Yelps.Common;
using Reader.FunctionalTests.Common;
using System.Net;
using Tests.Common.IntegrationEvents;
using Tests.Common.Networking;

namespace Reader.FunctionalTests.Trends;

[Collection(nameof(SharePersistanceInstance))]
public class GetTrendsEndpointTests : IClassFixture<ReaderApiTestFixture>
{
    private readonly ReaderApiTestFixture _fixture;

    public GetTrendsEndpointTests(ReaderApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetTrends_EnsureCorrectness()
    {
        var userId = Guid.NewGuid();

        _fixture.TriggerIntegrationEvent(
            new NewYelpIntegrationEvent(
                userId, "johnmclean", "John McLean", "av.jpg", "amazing content"));

        //Thread.Sleep(1000);

        var readerResponse = await RestApiCaller
            .GetAsync<List<YelpItem>>(_fixture.ApiClient, "/trends");

        Assert.Equal(HttpStatusCode.OK, readerResponse.Response.StatusCode);
        Assert.True(readerResponse.Value.Count > 0);
    }
}
