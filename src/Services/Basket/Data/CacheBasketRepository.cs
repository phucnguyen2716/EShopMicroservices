
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CacheBasketRepository 
    (IBasketRepository repository,IDistributedCache cache,CacheLockService lockService)
    : IBasketRepository
{
    private readonly SemaphoreSlim _lock = lockService.Lock;
    public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
    {
        var cacheBasket = await cache.GetStringAsync(UserName,cancellationToken);
        if(!string.IsNullOrEmpty(cacheBasket)) 
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
        }
        await _lock.WaitAsync(cancellationToken);
        try
        {
            var basket = await repository.GetBasket(UserName,cancellationToken);
            await cache.SetStringAsync(
                UserName,
                JsonSerializer.Serialize(basket),new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(30)
                },
                cancellationToken);

            return basket;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try{
            await repository.StoreBasket(basket,cancellationToken);
            await cache.SetStringAsync(basket.UserName,JsonSerializer.Serialize(basket),new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(30)
            },
            cancellationToken);
            return basket;
        }
        finally
        {
            _lock.Release();
        }
    }
     public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            await repository.DeleteBasket(UserName,cancellationToken);
            await cache.RemoveAsync(UserName,cancellationToken);
            return true;
        }
        finally
        {
            _lock.Release();
        }
       
    }
}