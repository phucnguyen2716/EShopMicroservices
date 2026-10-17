
using FastExpressionCompiler.ImTools;

namespace Basket.API.DeleteBasket;

public class DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket",async(string userName,ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(userName));
            var response = result.Adapt<DeleteBasketResult>();
            return Results.Ok(response);
        })
         .WithName("DeleteProduct")
        .DisableAntiforgery()
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}