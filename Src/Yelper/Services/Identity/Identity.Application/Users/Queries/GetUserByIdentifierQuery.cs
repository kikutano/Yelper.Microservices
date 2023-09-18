using ErrorOr;
using Identity.Application.Users.Common;
using MediatR;

namespace Identity.Application.Users.Queries;

public record GetUserByIdentifierQuery(string Identifier) : IRequest<ErrorOr<UserResult>>;
