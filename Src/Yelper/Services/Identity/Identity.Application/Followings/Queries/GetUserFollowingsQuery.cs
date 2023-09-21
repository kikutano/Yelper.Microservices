using ErrorOr;
using Identity.Application.Followings.Common;
using MediatR;

namespace Identity.Application.Followings.Queries;

public record GetUserFollowingsQuery(string At) : IRequest<ErrorOr<List<UserFollowingResult>>>;
