using Microsoft.AspNetCore.Routing.Internal;

namespace Discount.Grpc.Models;
public class Coupin
{
    public int Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Amount { get; set; }
}