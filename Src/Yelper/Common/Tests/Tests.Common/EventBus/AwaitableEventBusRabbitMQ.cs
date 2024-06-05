using EventBus.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQEventBus;

namespace Tests.Common.EventBus;

/*
 * #Issue
 * This implementation is used to await the publish pubblication. It's only for
 * testing purpose. To be reall honest I don't know if this is the right way
 * to do this thing. This because we are testing something that's different
 * from product environment. But I it's ok for now.  
 */
public class AwaitableEventBusRabbitMQ : EventBusRabbitMQ
{
    private TaskCompletionSource<bool> _taskCompletionSource = null!;

    public AwaitableEventBusRabbitMQ(
        IEventBusSubscriptionsManager subsManager,
        string? connectionString = null) : base(subsManager, connectionString)
    {
    }

    public new async Task Publish<T_Event>(T_Event integrationEvent)
    {
        _taskCompletionSource = new TaskCompletionSource<bool>();

        base.Publish(integrationEvent);

        await _taskCompletionSource.Task;
    }

    protected override void OnMessageReceivedHandler(object? sender, BasicDeliverEventArgs e)
    {
        base.OnMessageReceivedHandler(sender, e);
        _taskCompletionSource?.TrySetResult(true);
    }
}
