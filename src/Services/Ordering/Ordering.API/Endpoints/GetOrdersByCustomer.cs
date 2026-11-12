
using Ordering.API.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/orders/customer/{customerId}",async (Guid CustomerId,ISender sender) =>
       {
            var result = await sender.Send(new GetOrderByCustomerQuery(CustomerId));
            var response = result.Adapt<GetOrdersByCustomerResponse>();
            return Results.Ok(response);
       })
       .WithName("GetOrdersByCustomer")
       .Produces(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status401Unauthorized)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Get Orders By Customer")
       .WithDescription("Get Orders By Customer");
    }
}