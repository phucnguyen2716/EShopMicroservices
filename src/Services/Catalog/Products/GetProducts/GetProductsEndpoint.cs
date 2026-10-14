namespace Catalog.API.Products.GetProducts;

public record GetProductRequest(int? PageNumber=1,int? PageSize=10);

public record GetProductsRespone(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/products",async ([AsParameters]GetProductRequest request,ISender sender) =>
       {
            var query = request.Adapt<GetProductsQuery>();
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