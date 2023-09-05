using Identity.Domain.AggregatesModel.SecurityAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;

namespace Identity.Application.Users.Common;

public record UserCreatedResult(User User, Security Security);
