

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart cart);
    public class GetBasketHandler : IQueryHandler<GetBasketQuery,GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query , CancellationToken cancel)
        {
            return new GetBasketResult(new ShoppingCart("all item"));
        }
    }
}
