using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Common.ApiFactories;

[CollectionDefinition(nameof(ShareSameDatabaseInstance<TProgram, TDbContext>))]
public class ShareSameDatabaseInstance<TProgram, TDbContext>
    : ICollectionFixture<ApiTestFixture<TProgram, TDbContext>>
    where TProgram : class where TDbContext : DbContext
{
}
