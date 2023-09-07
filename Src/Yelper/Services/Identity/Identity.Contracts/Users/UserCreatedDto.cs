namespace Identity.Contracts.Users;

public record UserCreatedDto(Guid UserId, string Identifier, string Name, string AccessCode);
