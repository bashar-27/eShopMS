
using Basket.API.Data;

namespace Basket.API.Basket.GetAllBasket
{
    public record GetBasketQuer(): IQuery<GetBasketResult>;
    public record GetBasketResult(IEnumerable<ShoppingCart> Carts);
    public class GetAllBasketHandler(IBasketRepository basketRepo) : IQueryHandler<GetBasketQuer, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuer query, CancellationToken cancellationToken)
        {
           
            var allBaskets = await basketRepo.GetAllBaskets(cancellationToken); 
            return new GetBasketResult(allBaskets);
        }
    }
}
