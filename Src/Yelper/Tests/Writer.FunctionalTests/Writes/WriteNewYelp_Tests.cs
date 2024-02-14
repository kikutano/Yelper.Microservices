using System.Net;
using Tests.Common.ApiFactories;
using Tests.Common.Networking;
using Writer.Application.Common.Persistence;
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
        await _fixture.Auth("johnmclain", "1234");

        var response = await RestApiCaller
            .PostAsync<RestApiResponse>(_fixture.ApiClient, "api/v1/writer");

        Assert.Equal(HttpStatusCode.OK, response.Response.StatusCode);
    }
}
