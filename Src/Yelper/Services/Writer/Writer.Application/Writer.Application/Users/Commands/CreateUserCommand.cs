using MediatR;

namespace Writer.Application.Users.Commands;

public record CreateUserCommand(string At, string Name, string AvatarUrl) : IRequest;
