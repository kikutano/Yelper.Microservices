using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Persistence.QueryExtensions.Users;

public static class FromAtToIdExtension
{
    public async static Task<Guid> FromAtToIdAsync(
        this DbSet<User> users, string at, CancellationToken cancellationToken)
    {
        return await users
            .Where(x => x.At == at)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
