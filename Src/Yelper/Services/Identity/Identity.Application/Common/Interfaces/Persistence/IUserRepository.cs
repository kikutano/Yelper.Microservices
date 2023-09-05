using Identity.Domain.AggregatesModel.UserAggregate;

namespace Identity.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    public IEnumerable<User> GetAll(CancellationToken cancellationToken);
    public Task<User?> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken);
    public Task<Guid?> GetIdByIdentifierAsync(string identifier, CancellationToken cancellationToken);
    public Task CreateAsync(User user, CancellationToken cancellationToken);
    public Task<bool> ExistAsync(string identifier, CancellationToken cancellationToken);
    public Task SaveChangesAsync();
    public void Update(User user);
}
