namespace Identity.Application.Users.Common;

public record UserCreatedResult(UserResult User, string AccessCode);
