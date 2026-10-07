
namespace Catalog.API.Products.GetProdductsById;



public record GetProductByIdRespone(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{id:guid}", async (Guid id,ISender sender)=>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var respone = result.Adapt<GetProductByIdRespone>();
            return Results.Ok(respone);
        }) 
        .WithName("GetProductById")
        .DisableAntiforgery()
        .Produces<GetProductByIdRespone>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}