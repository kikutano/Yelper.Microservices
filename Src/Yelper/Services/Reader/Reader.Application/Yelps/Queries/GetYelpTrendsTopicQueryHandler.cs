using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Reader.Application.Yelps.Common;
using System.Text.Json;

namespace Reader.Application.Yelps.Queries;

public class GetYelpTrendsTopicQueryHandler : IRequestHandler<GetYelpTrendsTopicQuery, List<YelpItem>>
{
    private readonly IDistributedCache _distributedCache;

    public GetYelpTrendsTopicQueryHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<List<YelpItem>> Handle(GetYelpTrendsTopicQuery request, CancellationToken cancellationToken)
    {
        var result = await _distributedCache.GetStringAsync("trend", cancellationToken);

        if (result == null)
        {
            return new List<YelpItem>();
        }

        var yelpItem = JsonSerializer.Deserialize<YelpItem>(result!);

        return new List<YelpItem>() { yelpItem! };
    }
}
