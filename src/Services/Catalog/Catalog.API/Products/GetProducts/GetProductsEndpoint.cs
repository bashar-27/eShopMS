namespace Catalog.API.Products.GetProducts
{

    //public record GetProductsRequest();  No need for request record in this case because we are not passing any data to the endpoint
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", async (ISender sender) =>
            {
                var query = new GetProductsQuery();
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            })
            .WithDescription("Get all products")
            .WithName("GetProducts")
            .WithSummary("Get all products")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
   
}
