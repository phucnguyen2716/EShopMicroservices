
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Application.Orders.Queries.GetOrders;

public class GetOrderHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderQuery, GetOrderResult>
{
    public async Task<GetOrderResult> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .Include(order => order.OrderItems)
            .OrderBy(order => order.OrderName.Value)
            .Take(pageIndex)
            .Skip(pageSize*(pageIndex-1))
            .ToListAsync(cancellationToken);
        
        return new GetOrderResult(
            new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orders.ToDoOrderList()
            ));
    }
}