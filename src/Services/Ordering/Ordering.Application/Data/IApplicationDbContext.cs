using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems {get;}
    Task<int> SavingChangeAsync(CancellationToken  cancellationToken);
}