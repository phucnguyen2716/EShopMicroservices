namespace Ordering.API.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}",async (string orderName, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByNameQuery(orderName));
            var response = result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(response); 
        })
        .WithName("GetOrdersByName")
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Get Orders By Name")
        .WithSummary("Get Orders By Name");
    }
}