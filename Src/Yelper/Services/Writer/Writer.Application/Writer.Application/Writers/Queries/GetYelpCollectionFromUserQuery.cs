using MediatR;

namespace Writer.Application.Writers.Queries;

public record GetYelpCollectionFromUserQuery(Guid UserId)
    : IRequest<ICollection<YelpItemCollectionResponse>>;
