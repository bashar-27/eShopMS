namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(Product Product);
    internal class UpdateProductHandler (IDocumentSession session , ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id , cancellationToken);
            if (product is null )
            {
                throw new ProductNotFoundException(command.Id);
            }
            product.Name = command.Name;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            product.Category = command.Category;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(product);
        }
    }
    
}



//public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
//    : ICommand<UpdateProductResult>;
//public record UpdateProductResult(bool IsSuccess);

//internal class UpdateProductCommandHandler
//    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
//    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
//{
//    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
//    {
//        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

//        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

//        if (product is null)
//        {
//            throw new ProductNotFoundException();
//        }

//        product.Name = command.Name;
//        product.Category = command.Category;
//        product.Description = command.Description;
//        product.ImageFile = command.ImageFile;
//        product.Price = command.Price;

//        session.Update(product);
//        await session.SaveChangesAsync(cancellationToken);

//        return new UpdateProductResult(true);
//    }
//}