using BuildingBlocks.CQRS;

namespace Basket.API.Models.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

internal class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}