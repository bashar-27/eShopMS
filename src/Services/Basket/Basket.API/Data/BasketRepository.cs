

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            return basket is null? throw new BasektNotFoundException(userName) : basket;

        }
   
            public async Task<IEnumerable<ShoppingCart>> GetAllBaskets(CancellationToken cancellationToken = default)
            {
            // Fetch all baskets (you'll need to implement this based on your data source)
            var basket = await session.LoadAsync<ShoppingCart>(cancellationToken);
            // return allBaskets ?? Enumerable.Empty<ShoppingCart>();

            return (IEnumerable<ShoppingCart>)(basket is null ? throw new Exception(null) : basket);
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;

        }
    }
}
