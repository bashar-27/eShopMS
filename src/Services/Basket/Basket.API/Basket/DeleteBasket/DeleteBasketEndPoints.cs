
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/DeleteBasket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userName));
                var response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            })
                   .WithDescription("Delete Basket")
                   .WithName("Delete Basket")
                   .WithSummary("Delete product in basket")
                   .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                   .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}
