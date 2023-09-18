using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Persistence.QueryExtensions.Users;

public static class ExistByAtQueryExtension
{
    public async static Task<bool> ExistAsync(
        this DbSet<User> users, string at, CancellationToken cancellationToken)
    {
        return await users
            .Where(user => user.At == at)
            .AnyAsync(cancellationToken);
    }
}

public static class ExistQueryByUserIdExtension
{
    public async static Task<bool> ExistAsync(
        this DbSet<User> users, Guid userId, CancellationToken cancellationToken)
    {
        return await users
            .Where(user => user.Id == userId)
            .AnyAsync(cancellationToken);
    }
}
