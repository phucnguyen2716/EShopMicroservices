namespace Ordering.API.Domain.ValueOjects;

public record CustomerId
{
    public Guid Value { get; }
    private CustomerId (Guid value) => Value = value;
    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new DomainException("CustomerId cannot be null");
        return new CustomerId(value);
    }
}