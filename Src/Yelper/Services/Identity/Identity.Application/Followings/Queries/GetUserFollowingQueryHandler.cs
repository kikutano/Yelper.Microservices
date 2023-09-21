using ErrorOr;
using Identity.Application.Common.Persistence;
using Identity.Application.Common.Persistence.QueryExtensions.Users;
using Identity.Application.Followings.Common;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Followings.Queries;

public sealed class GetUserFollowingQueryHandler
    : IRequestHandler<GetUserFollowingsQuery, ErrorOr<List<UserFollowingResult>>>
{
    private readonly IdentityDbContext _dbContext;

    public GetUserFollowingQueryHandler(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<List<UserFollowingResult>>> Handle(
        GetUserFollowingsQuery request, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Users.ExistAsync(request.At, cancellationToken))
        {
            return Errors.User.AtNotFound(request.At);
        }

        var userId = await _dbContext
            .Users
            .FromAtToIdAsync(request.At, cancellationToken);

        var usersFollowings = await _dbContext
            .Followings
            .Include(x => x.User)
            .Where(x => x.UserId == userId)
            .Select(x => new UserFollowingResult(
                x.FollowingUser.At,
                x.FollowingUser.Name,
                x.FollowingUser.Bio,
                x.FollowingUser.AvatarUrl))
            .ToListAsync(cancellationToken);

        return usersFollowings;
    }
}
