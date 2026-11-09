
using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrderByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
{
    public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(order => order.OrderItems)
            .AsNoTracking()
            .Where(order => order.CustomerId == CustomerId.Of(query.CustomerId))
            .OrderBy(order => order.OrderName.Value)
            .ToListAsync(cancellationToken);
        
        return new GetOrderByCustomerResult(orders.ToDoOrderList());
    }
}