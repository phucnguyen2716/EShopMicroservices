namespace Ordering.API.Application.Orders.Commands.Queries;

public record GetOrderByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;

public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);