
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);

    
    internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger, IValidator<DeleteProductCommand> validator) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Delete product from DeleteProductHandler class @{command}", command);

            var resultValid = await validator.ValidateAsync(command, cancellationToken);
            var error = resultValid.Errors.Select(resultValid => resultValid.ErrorMessage).ToList();
            if (error.Any())
            {
                throw new ValidationException(error.FirstOrDefault());
            }

            // var product = session.Query<Product>().Where(p => p.Id == command.Id).FirstOrDefault();
            var product = session.Load<Product>(command.Id);
            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            session.Delete(product);
            session.SaveChanges();
            return new DeleteProductResult(true);
        }
    }

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(prodID => prodID.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }
}
