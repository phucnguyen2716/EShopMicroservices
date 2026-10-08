namespace Catalog.API.Products.GetProducts;

public record GetProductsRespone(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/products",async (ISender sender) =>
       {
            var result = await sender.Send(new GetProductsQuery());
            var respone = result.Adapt<GetProductsResult>();
            return Results.Ok(respone);
       }) 
        .WithName("GetProducts")
        .DisableAntiforgery()
        .Produces<GetProductsRespone>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}