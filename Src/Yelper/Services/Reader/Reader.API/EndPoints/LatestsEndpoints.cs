using MediatR;
using Reader.Application.Yelps.Queries;

namespace Reader.API.EndPoints;

public static class LatestsEndpoints
{
    public static void MapLatestsEndpoints(this WebApplication app)
    {
        app.MapGet("api/v1/latests/followed", () => { return "Not implemented yet!"; });

        app.MapGet("api/v1/latests", async (IMediator mediator)
            => await mediator.Send(new GetLatestYelpsQuery()));
    }
}
