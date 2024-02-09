using Tests.Common.ApiFactories;
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

    }
}
