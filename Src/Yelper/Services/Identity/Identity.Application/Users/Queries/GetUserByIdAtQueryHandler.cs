using ErrorOr;
using Identity.Application.Common.Persistence;
using Identity.Application.Users.Common;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Users.Queries;

public class GetUserByIdAtQueryHandler
    : IRequestHandler<GetUserByAtQuery, ErrorOr<UserResult>>
{
    private readonly IdentityDbContext _dbContext;

    public GetUserByIdAtQueryHandler(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<UserResult>> Handle(
        GetUserByAtQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext
            .Users
            .Where(user => user.At == request.At)
            .Select(user => new UserResult(
                user.Id,
                user.At,
                user.Name,
                user.AvatarUrl
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Errors.User.AtNotFound(request.At);
        }

        return user;
    }
}
