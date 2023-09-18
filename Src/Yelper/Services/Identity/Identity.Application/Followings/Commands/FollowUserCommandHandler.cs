using ErrorOr;
using Identity.Application.Common.Persistence;
using Identity.Application.Common.Persistence.QueryExtensions.Followings;
using Identity.Application.Common.Persistence.QueryExtensions.Users;
using Identity.Domain.AggregatesModel.FollowerAggregate;
using Identity.Domain.AggregatesModel.FollowingAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;

namespace Identity.Application.Followings.Commands;

public sealed class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, ErrorOr<Unit>>
{
    private readonly IdentityDbContext _dbContext;

    //togliere da qui
    //private readonly IValidator<FollowUserCommand> _commandValidator;

    public FollowUserCommandHandler(
        IdentityDbContext dbContext/*, IValidator<FollowUserCommand> validator*/)
    {
        _dbContext = dbContext;
        //_commandValidator = validator;
    }

    public async Task<ErrorOr<Unit>> Handle(
        FollowUserCommand request, CancellationToken cancellationToken)
    {
        //var result = await _commandValidator.ValidateAsync(request); //togliere, dovrebbe chiamaro da solo

        //if (result.Errors.Any())
        //{
        //    return Error.Failure("bad request");
        //}

        var validationResult = await ValidateCommandAsync(request, cancellationToken);

        if (validationResult.IsError)
        {
            return validationResult.Errors.FirstOrDefault();
        }

        var following = Following.Create(request.FromUserId, request.ToUserId);
        var follower = Follower.Create(request.ToUserId, request.FromUserId);

        await _dbContext.Followings.AddAsync(following.Value, cancellationToken);
        await _dbContext.Followers.AddAsync(follower.Value, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ErrorOr<Unit>();
    }
    private async Task<ErrorOr<bool>>
        ValidateCommandAsync(FollowUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Users.ExistAsync(request.FromUserId, cancellationToken))
        {
            return Errors.User.UserIdNotFound(request.FromUserId);
        }

        if (!await _dbContext.Users.ExistAsync(request.ToUserId, cancellationToken))
        {
            return Errors.User.UserIdNotFound(request.ToUserId);
        }

        if (await _dbContext
                .Followings
                .ExistAsync(request.FromUserId, request.ToUserId, cancellationToken))
        {
            return Error.Conflict($"User {request.FromUserId} already follow {request.ToUserId}");
        }

        return true;
    }
}
