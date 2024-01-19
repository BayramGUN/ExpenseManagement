using ExpenseManagement.Business.Common.Interfaces.EventBus;
using MassTransit;

namespace ExpenseManagement.Business.Common.Implementation.EventBus;

public class EventBus : IEventBus
{
    private readonly IPublishEndpoint publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(
        T message, 
        CancellationToken cancellationToken = default) where T : class =>
            publishEndpoint.Publish(message, cancellationToken);
}