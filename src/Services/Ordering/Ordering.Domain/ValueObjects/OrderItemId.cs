namespace Ordering.API.Domain.ValueOjects;

public record OrderItemId
{
    public Guid Value { get; }
    private OrderItemId (Guid value) => Value = value;
    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new DomainException("OrderItemId cannot be null");
        return new OrderItemId(value);
    }
}