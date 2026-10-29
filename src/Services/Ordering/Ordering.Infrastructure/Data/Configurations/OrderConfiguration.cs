using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.API.Domain.Models;

namespace Ordering.API.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
   public void Configure(EntityTypeBuilder<Order> builder)
    {
        throw new NotImplementedException();
    }
}