

namespace Basket.API.Basket.GetBasket
{
    //public record GetBasketRequest(string UserName)
    public record GetBasketResponse(ShoppingCart cart);
    public class GetBasketEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            })
            .WithDescription("Get Product By Id")
            .WithName("GetProductById")
            .WithSummary("Get Product By Id")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
