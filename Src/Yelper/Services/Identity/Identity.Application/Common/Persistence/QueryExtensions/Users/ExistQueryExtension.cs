using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Persistence.QueryExtensions.Users;

public static class ExistQueryExtension
{
    public async static Task<bool> ExistAsync(
        this DbSet<User> users, string identifier, CancellationToken cancellationToken)
    {
        return await users
            .Where(user => user.Identifier == identifier)
            .AnyAsync(cancellationToken);
    }
}
