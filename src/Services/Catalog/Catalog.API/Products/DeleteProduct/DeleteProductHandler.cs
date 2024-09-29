
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);
    internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Delete product from DeleteProductHandler class @{command}", command);

            // var product = session.Query<Product>().Where(p => p.Id == command.Id).FirstOrDefault();
            var product = session.Load<Product>(command.Id);
            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            session.Delete(product);
            session.SaveChanges();
            return Task.FromResult(new DeleteProductResult(true));
        }
    }
}
