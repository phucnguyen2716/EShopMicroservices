
using FastExpressionCompiler.ImTools;

namespace Basket.API.DeleteBasket;

public record DeleteBasketResponse(bool isSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}",async(string userName,ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(userName));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        })
         .WithName("DeleteBasket")
        .DisableAntiforgery()
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Basket")
        .WithDescription("Delete Basket");
    }
}