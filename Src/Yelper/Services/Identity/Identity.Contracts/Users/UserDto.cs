namespace Identity.Contracts.Users;

public record UserDto(
    Guid Id,
    string Name,
    string Identifier,
    string AvatarUrl,
    int Followers,
    int Following,
    int YelpsCount,
    DateTime SubscriptionAt);
