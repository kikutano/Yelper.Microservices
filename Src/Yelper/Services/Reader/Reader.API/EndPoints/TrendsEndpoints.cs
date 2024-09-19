using MediatR;
using Reader.Application.Yelps.Queries;

namespace Reader.API.EndPoints;

public static class TrendsEndpoints
{
    public static void MapTrendsEndpoints(this WebApplication app)
    {
        app.MapGet("api/v1/trends", async (IMediator mediator)
            => await mediator.Send(new GetYelpTrendsTopicQuery()));
    }
}
