using ErrorOr;
using MediatR;

namespace Writer.Application.Writers.Commands;

public record CreateYelpCommand(Guid UserId, string Text) : IRequest<ErrorOr<Unit>>;
