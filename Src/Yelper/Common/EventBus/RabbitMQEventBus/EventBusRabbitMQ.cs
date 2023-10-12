using EventBus.Events;
using EventBus.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RabbitMQEventBus;

public class EventBusRabbitMQ : IEventBus
{
    private IModel _channel = null!;
    private readonly string _queueName = "yelper_queue";
    private readonly string _exchangeName = "yelper_event_bus";
    private readonly IEventBusSubscriptionsManager _subsManager;

    public EventBusRabbitMQ(IEventBusSubscriptionsManager subsManager, string? connectionString = null)
    {
        _subsManager = subsManager;
        Connect(connectionString);
    }

    private void OnMessageReceivedHandler(object? sender, BasicDeliverEventArgs e)
    {
        var eventName = e.RoutingKey;
        var content = Encoding.UTF8.GetString(e.Body.Span);

        var subscriptions = _subsManager.GetHandlersForEvent(eventName);

        if (subscriptions is not null)
        {
            foreach (var eventHandler in subscriptions)
            {
                eventHandler.Handle(content);
            }
        }
    }

    public void Publish<T_Event>(T_Event integrationEvent)
    {
        var json = JsonSerializer.Serialize(integrationEvent);
        var body = Encoding.UTF8.GetBytes(json);
        var eventName = integrationEvent!.GetType().Name;

        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: eventName,
            mandatory: true,
            body: body);
    }

    public void Subscribe<T_Event, T_Handler>()
        where T_Event : IntegrationEvent
        where T_Handler : IIntegrationEventHandler
    {
        _subsManager.AddSubscription<T_Event, T_Handler>();

        var eventName = typeof(T_Event).Name;
        _channel.QueueBind(
            queue: _queueName,
            exchange: _exchangeName,
            routingKey: eventName);
    }

    public void Connect(string? connectionString = null)
    {
        try
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
            };

            if (!string.IsNullOrEmpty(connectionString))
            {
                factory.Uri = new Uri(connectionString);
            }

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(
                exchange: _exchangeName,
                type: "direct");

            _channel.QueueDeclare(
                queue: _queueName,
                durable: false,
                exclusive: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += OnMessageReceivedHandler;

            _channel.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            //log execption
        }
    }
}
