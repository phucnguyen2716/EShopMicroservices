namespace Ordering.API.Domain.ValueOjects;

public record OrderId
{
    public Guid Value { get; }   
    private OrderId (Guid value) => Value = value;
    public static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new DomainException("OrderId cannot be null");
        return new OrderId(value);
    }
}