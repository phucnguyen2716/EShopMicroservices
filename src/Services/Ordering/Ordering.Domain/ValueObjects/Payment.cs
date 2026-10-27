using Ordering.API.Domain.Abstractions;

namespace Ordering.API.Domain.ValueOjects;

public class Payment : Entity <Guid>
{
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default;
}