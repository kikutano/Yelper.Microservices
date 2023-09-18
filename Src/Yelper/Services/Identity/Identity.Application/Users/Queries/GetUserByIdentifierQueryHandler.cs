using ErrorOr;
using Identity.Application.Common.Persistence;
using Identity.Application.Users.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

using UserErrors = Identity.Domain.AggregatesModel.UserAggregate;

namespace Identity.Application.Users.Queries;

public class GetUserByIdentifierQueryHandler
    : IRequestHandler<GetUserByIdentifierQuery, ErrorOr<UserResult>>
{
    private readonly IdentityDbContext _dbContext;

    public GetUserByIdentifierQueryHandler(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<UserResult>> Handle(
        GetUserByIdentifierQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext
            .Users
            .Where(user => user.Identifier == request.Identifier)
            .Select(user => new
            {
                user.Id,
                user.Identifier,
                user.Name,
                user.AvatarUrl
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return UserErrors.Errors.User.IdentifierNotFound(request.Identifier);
        }

        return new UserResult(
            user.Id,
            user.Identifier,
            user.Name,
            user.AvatarUrl);
    }
}
