namespace Reader.Application.Yelps.Common;

public record YelpItem(
   Guid Id, Guid UserId, string Content, DateTime CreatedAt);
