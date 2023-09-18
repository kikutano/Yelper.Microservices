using ErrorOr;
using Identity.Application.Users.Common;
using MediatR;

namespace Identity.Application.Users.Queries;

public record GetUserByAtQuery(string At) : IRequest<ErrorOr<UserResult>>;
