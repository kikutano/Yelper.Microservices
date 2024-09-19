using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Reader.Application.Yelps.Commands;

public class AddNewYelpToTrendTopicCommandHandler : IRequestHandler<AddNewYelpToTrendTopicsCommand>
{
    private readonly IDistributedCache _distributedCache;

    public AddNewYelpToTrendTopicCommandHandler(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task Handle(AddNewYelpToTrendTopicsCommand request, CancellationToken cancellationToken)
    {
        string content = JsonSerializer.Serialize(request);

        await _distributedCache.SetStringAsync("trend", content, cancellationToken);

        var result = await _distributedCache.GetStringAsync("trend", cancellationToken);

        int a = 0;
    }
}
