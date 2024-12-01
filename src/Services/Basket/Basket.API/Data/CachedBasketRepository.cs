
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepo , IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basketUserCache = await cache.GetStringAsync(userName, cancellationToken);
            if(!string.IsNullOrEmpty(basketUserCache))
                await cache.RemoveAsync(userName, cancellationToken);

            var basketUserDB = await basketRepo.GetBasket(userName, cancellationToken);
            if (basketUserDB is not null)
            await basketRepo.DeleteBasket(userName, cancellationToken);
            
            return true;
        }

        public Task<IEnumerable<ShoppingCart>> GetAllBaskets(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basketCached = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(basketCached))
                 return JsonSerializer.Deserialize<ShoppingCart>(basketCached)!;
            var baseket= await basketRepo.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(baseket), cancellationToken);
            return baseket;

       }

     

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            
             await basketRepo.StoreBasket(basket, cancellationToken);
             await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
    }
}
