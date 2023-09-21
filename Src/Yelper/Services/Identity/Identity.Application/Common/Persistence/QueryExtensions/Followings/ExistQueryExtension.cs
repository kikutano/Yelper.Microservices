using Identity.Domain.AggregatesModel.FollowingAggregate;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Persistence.QueryExtensions.Followings;

public static class ExistQueryExtension
{
    public async static Task<bool> ExistAsync(
        this DbSet<Following> followings,
        Guid fromUserId,
        Guid toUserId,
        CancellationToken cancellationToken)
    {
        return await followings
            .Where(user => user.UserId == fromUserId)
            .Where(user => user.FollowingUserId == toUserId)
            .AnyAsync(cancellationToken);
    }
}