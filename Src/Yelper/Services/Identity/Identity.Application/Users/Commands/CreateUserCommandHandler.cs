using ErrorOr;
using Identity.Application.Common.Interfaces.Persistence;
using Identity.Application.Users.Common;
using Identity.Domain.AggregatesModel.SecurityAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;

namespace Identity.Application.Users.Commands;

internal sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, ErrorOr<UserCreatedResult>>
{
    public readonly IUserRepository _userRepository;
    public readonly ISecurityRepository _securityRepository;

    public CreateUserCommandHandler(
        IUserRepository userRepository, ISecurityRepository securityRepository)
    {
        _userRepository = userRepository;
        _securityRepository = securityRepository;
    }

    public async Task<ErrorOr<UserCreatedResult>> Handle(
        CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistAsync(request.Identifier, cancellationToken))
        {
            return Errors.User.IdentifierDuplicate(request.Identifier);
        }

        var user = User.Create(request.Name, request.Identifier);

        if (user.Errors.Any())
        {
            return user.Errors.First();
        }

        var security = Security.Create(user.Value.Id);

        if (security.Errors.Any())
        {
            return security.Errors.First();
        }

        await _userRepository.CreateAsync(user.Value, cancellationToken);
        await _securityRepository.CreateAsync(security.Value, cancellationToken);
        await _userRepository.SaveChangesAsync();

        return new UserCreatedResult(user.Value, security.Value);
    }
}
