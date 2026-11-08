using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.API.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
       logger.LogInformation("Event Event handled: {DomainEvent}",notification.GetType().Name);
        return Task.CompletedTask;
    }
}