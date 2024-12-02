
namespace Basket.API.Basket.GetAllBasket
{
    public record GetBasketResponse(ShoppingCart cart);
    public class GetAllBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("basket/", async (ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuer());
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            }).WithDescription("Get Product")
            .WithName("GetProduct")
            .WithSummary("Get Product")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        }
    }
}
