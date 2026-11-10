

namespace Ordering.API.Endpoints;

public record GetOrderResponse(PaginatedResult<OrderDto> Orders);

public class GetOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders",async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderQuery(request));
            var response = result.Adapt<GetOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProduct")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Get Product")
        .WithSummary("Get Prodduct");
    }
}
