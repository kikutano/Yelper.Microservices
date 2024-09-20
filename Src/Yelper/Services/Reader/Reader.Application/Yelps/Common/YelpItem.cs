namespace Reader.Application.Yelps.Common;

public record YelpItem(
    Guid UserId, string At, string Name, string AvatarUrl, string Text);
