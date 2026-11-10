
namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/orders/customer/{customerId}",async (Guid customerId,ISender sender) =>
       {
            var result = await sender.Send(customerId);
            var response = result.Adapt<GetOrdersByCustomer>();
       })
       .WithName("GetOrdersByCustomer")
       .Produces(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status401Unauthorized)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Get Orders By Customer")
       .WithDescription("Get Orders By Customer");
    }
}