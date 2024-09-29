
namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdRequest(Guid Id) : IRequest<GetProductByIdResponse>;
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (ISender sender, Guid id) =>
            {
              //  var query = new GetProductByIdRequest(id);
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);

            }).Accepts<Guid>("The product ID")
                .WithDescription("Get a product by ID")
                .WithName("GetProductById")
                .WithSummary("Get a product by ID")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
       
        }
}
