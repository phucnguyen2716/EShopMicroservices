
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id,string Name,List<string> Category,string Description,string ImageFile,decimal Price);
public record UpdateProductResponse(Product Product);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/product/update/{id:guid}",async (UpdateProductRequest request,ISender sender)=>
        {
            var command = request.Adapt<UpdateProductResult>();
            var respone = sender.Send(command);
            var result = respone.Adapt<UpdateProductResponse>();
            return Results.Ok(result);
        })
        .WithName("Update Product")
        .DisableAntiforgery()
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}