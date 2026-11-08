using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.API.Application.Orders.Commands.EventHandlers;

public class OrderCreatedEventHandler (ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event Event handled: {DomainEvent}",notification.GetType().Name);
        return Task.CompletedTask;
    }
}