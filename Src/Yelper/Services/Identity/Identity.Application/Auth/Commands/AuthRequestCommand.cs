using ErrorOr;
using Identity.Application.Auth.Common;
using MediatR;

namespace Identity.Application.Auth.Commands;

public record AuthRequestCommand(string At, string AccessCode) : IRequest<ErrorOr<AuthRequestResult>>;
