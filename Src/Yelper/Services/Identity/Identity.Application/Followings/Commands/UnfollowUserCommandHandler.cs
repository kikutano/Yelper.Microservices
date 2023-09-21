using ErrorOr;
using Identity.Application.Common.Persistence;
using Identity.Application.Common.Persistence.QueryExtensions.Followings;
using Identity.Application.Common.Persistence.QueryExtensions.Users;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Followings.Commands;

internal sealed class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, ErrorOr<Unit>>
{
    private readonly IdentityDbContext _dbContext;

    public UnfollowUserCommandHandler(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Unit>> Handle(
        UnfollowUserCommand request, CancellationToken cancellationToken)
    {
        //TODO: Add validators!
        var validationResult = await ValidateCommandAsync(request, cancellationToken);

        if (validationResult.IsError)
        {
            return validationResult.Errors.FirstOrDefault();
        }

        var followingId = await _dbContext
            .Followings
            .Where(x => x.UserId == request.FromUserId)
            .Where(x => x.FollowingUserId == request.ToUserId)
            .ExecuteDeleteAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ErrorOr<Unit>();
    }

    private async Task<ErrorOr<bool>>
        ValidateCommandAsync(UnfollowUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Users.ExistAsync(request.FromUserId, cancellationToken))
        {
            return Errors.User.UserIdNotFound(request.FromUserId);
        }

        if (!await _dbContext.Users.ExistAsync(request.ToUserId, cancellationToken))
        {
            return Errors.User.UserIdNotFound(request.ToUserId);
        }

        if (!await _dbContext
                .Followings
                .ExistAsync(request.FromUserId, request.ToUserId, cancellationToken))
        {
            return Error.Conflict($"User {request.FromUserId} already not follow {request.ToUserId}");
        }

        return true;
    }
}
