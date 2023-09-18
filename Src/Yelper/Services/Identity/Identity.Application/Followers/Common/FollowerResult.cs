namespace Identity.Application.Followers.Common;

public record FollowerResult(
    string Name,
    string At,
    string Bio,
    string AvatarUrl,
    DateTime CreatedAt,
    bool Following);
