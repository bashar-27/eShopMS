

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category): ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id) ;
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var prouct = new Product
            {
               
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile,
                Category = command.Category
            };
            session.Store(prouct);
            await session.SaveChangesAsync(cancellationToken);
            // Save the product to the database
            return new CreateProductResult(prouct.Id);
        }
    }
}
