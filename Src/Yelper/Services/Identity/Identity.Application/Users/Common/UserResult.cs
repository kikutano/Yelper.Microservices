namespace Identity.Application.Users.Common;

public record UserResult(Guid UserId, string At, string Name, string AvatarUrl);
