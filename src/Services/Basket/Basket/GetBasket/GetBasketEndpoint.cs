
using Basket.API.Models.GetBasket;

namespace Basket.API.GetBasket;

public record GetBasketRespone(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/basket/{userName}",async (string userName,ISender sender) =>
       {
           var result= await sender.Send(new GetBasketQuery(userName));
           var response = result.Adapt<GetBasketRespone>();
           return Results.Ok(response);
       })
        .WithName("GetBasketByUserName")
        .DisableAntiforgery()
        .Produces<GetBasketRespone>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket By UserName")
        .WithDescription("Get Basket By UserName");
    }
}