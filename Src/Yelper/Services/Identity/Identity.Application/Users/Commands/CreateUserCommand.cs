using ErrorOr;
using Identity.Application.Users.Common;
using MediatR;

namespace Identity.Application.Users.Commands;

public record CreateUserCommand(string At, string Name) : IRequest<ErrorOr<UserCreatedResult>>;
