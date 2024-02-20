using MediatR;
using Microsoft.EntityFrameworkCore;
using Writer.Application.Common.Persistence;

namespace Writer.Application.Writers.Queries;

public class GetYelpCollectionUserQueryHandler
    : IRequestHandler<GetYelpCollectionFromUserQuery, ICollection<YelpItemCollectionResponse>>
{
    private readonly WriterDbContext _dbContext;

    public GetYelpCollectionUserQueryHandler(WriterDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<YelpItemCollectionResponse>> Handle(
        GetYelpCollectionFromUserQuery request, CancellationToken cancellationToken)
    {
        var yelps = await _dbContext
            .Yelps
            .Where(x => x.UserId == request.UserId)
            .Select(x => new YelpItemCollectionResponse(x.CreatedAt, x.Text))
            .ToListAsync(cancellationToken);

        return yelps;
    }
}
