using ErrorOr;
using MediatR;

namespace Identity.Application.Followings.Commands;

public record UnfollowUserCommand(Guid FromUserId, Guid ToUserId) : IRequest<ErrorOr<Unit>>;
