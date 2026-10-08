namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryRespone(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/category/{category}",async(string category,ISender sender)=>
        {
           var result= await sender.Send(new GetProductByCategoryQuery(category));
           var respone = result.Adapt<GetProductByCategoryRespone>();
           return Results.Ok(respone);
        })
        .WithName("GetProductByCategory")
        .DisableAntiforgery()
        .Produces<GetProductByCategoryRespone>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}