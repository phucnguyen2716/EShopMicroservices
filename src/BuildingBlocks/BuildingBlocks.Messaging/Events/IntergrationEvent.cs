namespace BuildingBlocks.Messaging.Events;

public record IntergrationEvent
{
    public static Guid Id => Guid.NewGuid();
    public static DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName!;
}