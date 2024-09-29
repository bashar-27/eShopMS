
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommandRequest(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category);
    public record UpdateProductResponse(Product Product);
    public class UpdateProductEndpoint : ICarterModule
    {
            public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPut("/updateProduct" , async(ISender sender,  UpdateProductCommandRequest request) =>
           {
               var command = request.Adapt<UpdateProductCommand>();

               var result = await sender.Send(command);

               var response = result.Adapt<UpdateProductResponse>();

               return Results.Ok(response);


           }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
    
}
