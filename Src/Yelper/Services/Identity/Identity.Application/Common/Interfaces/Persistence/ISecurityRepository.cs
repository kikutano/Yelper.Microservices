using Identity.Domain.AggregatesModel.SecurityAggregate;

namespace Identity.Application.Common.Interfaces.Persistence;

public interface ISecurityRepository
{
    public Task CreateAsync(Security security, CancellationToken cancellationToken);
    public Task<bool> IsAccessValidAsync(Guid userId, string accessCode, CancellationToken cancellationToken);
    public Task SaveChangesAsync();
}
