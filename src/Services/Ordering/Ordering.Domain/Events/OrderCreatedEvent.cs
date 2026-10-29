namespace Ordering.API.Domain.Events;

public record OrderCreatedEvent(Order order) : IDomainEvent;