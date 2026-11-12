using Microsoft.EntityFrameworkCore;


namespace Ordering.API.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o=> o.OrderName.Value == query.Name)
            .OrderBy(o=>o.OrderName.Value)
            .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToDoOrderList());
    }

}