
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, decimal Price, string ImageFile, List<string> Category);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result =await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/l/{response.Id}", response);
            })
            .WithDescription("Create a new product")
            .WithName("CreateProduct")
            .WithSummary("Create a new product")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
              
        }
    }
}
