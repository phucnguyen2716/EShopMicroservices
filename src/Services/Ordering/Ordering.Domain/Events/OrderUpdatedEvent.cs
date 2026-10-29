namespace Ordering.API.Domain.Events;

public record OrderUpdatedEvent(Order order) : IDomainEvent;