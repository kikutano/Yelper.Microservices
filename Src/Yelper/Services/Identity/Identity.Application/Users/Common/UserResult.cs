namespace Identity.Application.Users.Common;

public record UserResult(Guid Id, string Identifier, string Name, string AvatarUrl);
