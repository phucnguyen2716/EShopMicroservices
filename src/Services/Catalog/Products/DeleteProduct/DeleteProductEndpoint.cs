
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSucess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/product/{id:guid}",async(Guid id,ISender sender)=>
        {
           var request = await sender.Send(new DeleteProductCommand(id));
           var respone = request.Adapt<DeleteProductResponse>();
            return Results.Ok(respone);
        }) 
        .WithName("DeleteProduct")
        .DisableAntiforgery()
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}