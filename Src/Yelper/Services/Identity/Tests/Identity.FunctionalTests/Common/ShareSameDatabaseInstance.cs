namespace Identity.FunctionalTests.Common;

[CollectionDefinition(nameof(ShareSameDatabaseInstance))]
public class ShareSameDatabaseInstance : ICollectionFixture<IdentityApiTestFixture>
{
}
