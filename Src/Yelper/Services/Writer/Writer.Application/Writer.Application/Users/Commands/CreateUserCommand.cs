using MediatR;

namespace Writer.Application.Users.Commands;

public record CreateUserCommand(
    Guid UserId, string At, string Name, string AvatarUrl) : IRequest;
