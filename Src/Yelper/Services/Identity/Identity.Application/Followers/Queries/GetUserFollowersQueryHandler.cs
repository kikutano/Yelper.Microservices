using ErrorOr;
using Identity.Application.Common.Persistence;
using Identity.Application.Common.Persistence.QueryExtensions.Users;
using Identity.Application.Followers.Common;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Followers.Queries;

internal class GetUserFollowersQueryHandler
    : IRequestHandler<GetUserFollowersQuery, ErrorOr<FollowersResult>>
{
    private readonly IdentityDbContext _dbContext;

    public GetUserFollowersQueryHandler(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<FollowersResult>> Handle(
        GetUserFollowersQuery request, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Users.ExistAsync(request.At, cancellationToken))
        {
            return Errors.User.AtNotFound(request.At);
        }

        var userId = await _dbContext
            .Users
            .FromAtToIdAsync(request.At, cancellationToken);

        var followers = await _dbContext
            .Followers
            .Include(x => x.User)
            .Where(x => x.Id == userId)
            .Select(x => new FollowerResult(
                x.User.Name,
                x.User.At,
                x.User.Bio,
                x.User.AvatarUrl,
                x.CreatedAt,
                x.Following))
            .ToListAsync(cancellationToken);

        return new FollowersResult(followers);
    }
}
