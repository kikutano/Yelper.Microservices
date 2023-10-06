namespace RabbitMQEventBus;

public class SubscriptionInfo
{
	public Type HandlerType { get; private set; }

	public SubscriptionInfo(Type handlerType)
	{
		HandlerType = handlerType;
	}
}
