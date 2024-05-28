using MediatR;

namespace Reader.Application.Yelps.Commands;

public record AddNewYelpToTrendTopicsCommand(
    Guid UserId, string At, string Name, string AvatarUrl, string Content) : IRequest;
