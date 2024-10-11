using MediatR;

namespace Reader.Application.Yelps.Commands;

public record ArchiveNewYelpCommand(
	Guid Id, Guid UserId, string Content, DateTime CreatedAt) : IRequest;
