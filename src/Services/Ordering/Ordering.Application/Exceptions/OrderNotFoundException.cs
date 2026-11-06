using BuildingBlocks.Exceptions;

namespace Ordering.API.Application.Exceptions;
public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid orderId) : base("Order",orderId)
    {}   
}