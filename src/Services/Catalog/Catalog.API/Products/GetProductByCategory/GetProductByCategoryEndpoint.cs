
namespace Catalog.API.Products.GetProductByCategory
{
    //public record GetProductByCategoryRequest();
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/productsByCategory/{category}", async (ISender sender, string category) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);

            }).Accepts<string>("The product category")
                .WithDescription("Get a product by category")
                .WithName("GetProductByCategory")
                .WithSummary("Get a product by category")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
