using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<CouponModel> Coupons { get; set; } = default!;
    public DiscountContext (DbContextOptions<DiscountContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CouponModel>().HasData(
            new CouponModel { Id = 1, ProductName = "iPhone 17", Description = "Discount 10%", Amount = 10 },
            new CouponModel  { Id = 2, ProductName = "Samsung Galaxy S25", Description = "Discount 15%", Amount = 15 },
            new CouponModel  { Id = 3, ProductName = "MacBook Air M4", Description = "Discount 20%", Amount = 20 },
            new CouponModel  { Id = 4, ProductName = "iPad Air", Description = "Discount 5%", Amount = 5 },
            new CouponModel  { Id = 5, ProductName = "Dell XPS 13", Description = "Discount 12%", Amount = 12 },
            new CouponModel  { Id = 6, ProductName = "AirPods Pro", Description = "Discount 8%", Amount = 8 },
            new CouponModel  { Id = 7, ProductName = "Sony WH-1000XM5", Description = "Discount 18%", Amount = 18 },
            new CouponModel  { Id = 8, ProductName = "Apple Watch", Description = "Discount 10%", Amount = 10 },
            new CouponModel  { Id = 9, ProductName = "ASUS ROG", Description = "Discount 25%", Amount = 25 },
            new CouponModel  { Id = 10, ProductName = "Logitech Mouse", Description = "Discount 7%", Amount = 7 }
            
        );
    }
}