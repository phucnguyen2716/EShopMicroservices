
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace Ordering.API.Application.Orders.Commands.EventHandlers.Domain;

public class OrderCreatedEventHandler 
    (IPublishEndpoint publishEndpoint,IFeatureManager featureManager,Logger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event Event handled: {DomainEvent}",domainEvent.GetType().Name);
        if(await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderCreatedIntergrationEvent = domainEvent.order.ToOrderDto();
            await publishEndpoint.Publish(orderCreatedIntergrationEvent,cancellationToken);
        }
    }
}