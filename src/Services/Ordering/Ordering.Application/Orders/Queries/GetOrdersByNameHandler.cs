using Microsoft.EntityFrameworkCore;
using Ordering.API.Application.Extensions;
using Ordering.API.Application.Orders.Commands.Queries;

namespace Ordering.API.Application.Orders.Commands;

public class GetOrdersByNameHandler (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o=>o.OrderName.Value.Contains(query.Name))
            .OrderBy(o=>o.OrderName)
            .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToDoOrderList());
    }

}