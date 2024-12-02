namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(Product Product);

    //Here I create a validator for the UpdateProductCommand class to validate the properties of the command. but in CreateProduct I create ProductCommandValidator external class not in the same block.
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(produt => produt.Id).NotEmpty().WithMessage("Product ID is required");
            RuleFor(product => product.Name).NotEmpty().WithMessage("Name is required").Length(2, 100).WithMessage("Name size must be between 2 and 150");
            RuleFor(product => product.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(product => product.ImageFile).NotEmpty().WithMessage("Image is required");
            RuleFor(product => product.Price).GreaterThan(0).WithMessage("Price is required and should be greater than 0");
        }

        internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger , IValidator<UpdateProductCommand> validator) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
        {
            public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var result = await validator.ValidateAsync(command, cancellationToken);
                var error = result.Errors.Select(result => result.ErrorMessage).ToList();
                if (error.Any())
                {
                    throw new ValidationException(error.FirstOrDefault());
                }
                var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
                if (product is null)
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
}
