using BuildingBlocks.Pagination;
namespace Ordering.API.Application.Orders.Queries.GetOrders;

public record GetOrderQuery (PaginationRequest PaginationRequest) : IQuery<GetOrderResult>;

public record GetOrderResult (PaginatedResult<OrderDto> Orders);
