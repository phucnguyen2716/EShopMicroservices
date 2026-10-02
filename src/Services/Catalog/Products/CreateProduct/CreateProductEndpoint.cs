using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name,List<string> Category,string Description,IFormFile ImageFile,decimal Price);
public record CreateProductRespone(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async ([FromForm]CreateProductRequest request, ISender sender)=>
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Category,
                request.Description,
                request.ImageFile,
                request.Price
            );

            var result = await sender.Send(command);

            var respone = result.Adapt<CreateProductRespone>();
            return Results.Created($"/products/{respone.Id}",respone);
        })
        .WithName("Create Product")
        .DisableAntiforgery()
        .Produces<CreateProductRespone>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}