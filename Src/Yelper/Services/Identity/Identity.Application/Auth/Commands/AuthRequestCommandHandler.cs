using ErrorOr;
using Identity.Application.Auth.Common;
using Identity.Application.Common.Auth;
using Identity.Application.Common.Persistence;
using Identity.Application.Common.Persistence.QueryExtensions.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ErrorsSecurity = Identity.Domain.AggregatesModel.SecurityAggregate.Errors;
using ErrorsUser = Identity.Domain.AggregatesModel.UserAggregate.Errors;

namespace Identity.Application.Auth.Commands;

internal class AuthRequestCommandHandler
    : IRequestHandler<AuthRequestCommand, ErrorOr<AuthRequestResult>>
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly IdentityDbContext _dbContext;

    public AuthRequestCommandHandler(
        IAuthService authService,
        IConfiguration configuration,
        IdentityDbContext dbContext)
    {
        _authService = authService;
        _configuration = configuration;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<AuthRequestResult>> Handle(
        AuthRequestCommand request, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Users.ExistAsync(request.At, cancellationToken))
        {
            return ErrorsUser.User.AtNotFound(request.At);
        }

        var userId = await _dbContext
            .Users
            .FromAtToIdAsync(request.At, cancellationToken);

        if (!await IsAccessCodeCorrectAsync(userId, request.AccessCode, cancellationToken))
        {
            return ErrorsSecurity.Security.AccessCodeIsNotCorrect();
        }

        var token = GenerateAccessToken(userId, request.At);

        return await Task.FromResult(new AuthRequestResult(token));
    }

    private string GenerateAccessToken(Guid userId, string identifier)
    {
        return _authService.GenerateToken(userId, identifier, _configuration);
    }

    private async Task<bool> IsAccessCodeCorrectAsync(
        Guid userId, string accessCode, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Securities
            .AnyAsync(x => x.UserId == userId && x.AccessCode == accessCode, cancellationToken);
    }
}