namespace Ordering.API.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrderByCustomerQuery (Guid CustomerId) 
    : IQuery<GetOrderByCustomerResult>;

public record GetOrderByCustomerResult(IEnumerable<OrderDto> Orders);

