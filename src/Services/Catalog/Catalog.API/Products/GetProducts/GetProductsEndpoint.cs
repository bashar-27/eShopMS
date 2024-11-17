namespace Catalog.API.Products.GetProducts
{

    public record GetProductsRequest(int? pageNumber = 1 , int? pageSize = 5); // No need for request record in this case because we are not passing any data to the endpoint but we need it in case we using pagenation
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/", async ([AsParameters] GetProductsRequest request ,ISender sender) =>
            {
                var query =  request.Adapt<GetProductsQuery>();
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
