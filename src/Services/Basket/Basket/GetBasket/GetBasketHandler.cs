namespace Basket.API.Models.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

internal class GetBasketHandler (IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket= await repository.GetBasket(query.UserName);
       return new GetBasketResult(basket);
    }
}