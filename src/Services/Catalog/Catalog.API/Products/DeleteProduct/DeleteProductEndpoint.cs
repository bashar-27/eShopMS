
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest (Guid Id);
    public record DeleteProductResponse (bool IsDeleted);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/deleteProduct/{id}", async (ISender sender, Guid Id) =>
            {
                var command = new DeleteProductCommand(Id);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);

            }).Accepts<Guid>("The product Id")
                .WithDescription("Delete a product")
                .WithName("DeleteProduct")
                .WithSummary("Delete a product")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}
