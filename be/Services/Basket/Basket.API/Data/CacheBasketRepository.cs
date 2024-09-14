using System.Text.Json;
using Basket.API.Configurations;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Basket.API.Data;

//TODO - Need to test redisOptions. Check the configuration on Program.cs
public class CacheBasketRepository(IBasketRepository repository, IDistributedCache cache, IOptions<RedisOptions> redisOptions)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);
        
        if(!string.IsNullOrEmpty(cacheBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket) ?? throw new InvalidOperationException();
        }
        
        var basket = await repository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasket(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);
        
        return true;
    }
}