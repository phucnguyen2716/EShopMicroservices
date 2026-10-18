namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart Cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default);
}