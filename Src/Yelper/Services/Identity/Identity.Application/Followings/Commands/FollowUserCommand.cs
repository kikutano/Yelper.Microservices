using ErrorOr;
using MediatR;

namespace Identity.Application.Followings.Commands;

public record FollowUserCommand(Guid FromUserId, Guid ToUserId) : IRequest<ErrorOr<Unit>>;
