using Microsoft.VisualStudio.TestPlatform.TestHost;
using Tests.Common.ApiFactories;
using Writer.Application.Common.Persistence;

namespace Writer.FunctionalTests.Common;

public class WriterApiTestFixture : ApiTestFixture<Program, WriterDbContext>
{
}
