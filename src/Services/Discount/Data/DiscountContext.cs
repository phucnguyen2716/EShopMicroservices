using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    public DiscountContext (DbContextOptions<DiscountContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "iPhone 17", Description = "Discount 10%", Amount = 10 },
            new Coupon { Id = 2, ProductName = "Samsung Galaxy S25", Description = "Discount 15%", Amount = 15 },
            new Coupon  { Id = 3, ProductName = "MacBook Air M4", Description = "Discount 20%", Amount = 20 },
            new Coupon  { Id = 4, ProductName = "iPad Air", Description = "Discount 5%", Amount = 5 },
            new Coupon  { Id = 5, ProductName = "Dell XPS 13", Description = "Discount 12%", Amount = 12 },
            new Coupon  { Id = 6, ProductName = "AirPods Pro", Description = "Discount 8%", Amount = 8 },
            new Coupon  { Id = 7, ProductName = "Sony WH-1000XM5", Description = "Discount 18%", Amount = 18 },
            new Coupon  { Id = 8, ProductName = "Apple Watch", Description = "Discount 10%", Amount = 10 },
            new Coupon  { Id = 9, ProductName = "ASUS ROG", Description = "Discount 25%", Amount = 25 },
            new Coupon  { Id = 10, ProductName = "Logitech Mouse", Description = "Discount 7%", Amount = 7 }

        );
    }
}