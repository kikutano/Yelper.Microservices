using ErrorOr;
using Identity.Application.Common.Persistence;
using Identity.Application.Common.Persistence.QueryExtensions.Users;
using Identity.Application.Users.Common;
using Identity.Domain.AggregatesModel.SecurityAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using ErrorsUser = Identity.Domain.AggregatesModel.UserAggregate.Errors;

namespace Identity.Application.Users.Commands;

internal sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, ErrorOr<UserCreatedResult>>
{
    private readonly IdentityDbContext _dbContext;

    public CreateUserCommandHandler(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<UserCreatedResult>> Handle(
        CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Users.ExistAsync(request.At, cancellationToken))
        {
            return ErrorsUser.User.AtDuplicate(request.At);
        }

        var user = User.Create(request.Name, request.At);

        if (user.ErrorsOrEmptyList.Any())
        {
            return user.Errors.First();
        }

        var security = Security.Create(user.Value.Id);

        if (security.ErrorsOrEmptyList.Any())
        {
            return security.Errors.First();
        }

        await _dbContext.Users.AddAsync(user.Value, cancellationToken);
        await _dbContext.Securities.AddAsync(security.Value, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UserCreatedResult(
            new UserResult(
                user.Value.Id,
                user.Value.At,
                user.Value.Name,
                user.Value.AvatarUrl),
                security.Value.AccessCode);
    }
}
