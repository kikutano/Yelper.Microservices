using ErrorOr;
using Identity.Application.Followers.Common;
using MediatR;

namespace Identity.Application.Followers.Queries;

public record GetUserFollowersQuery(string At) : IRequest<ErrorOr<List<FollowerResult>>>;
