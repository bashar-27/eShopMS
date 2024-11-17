

using Catalog.API.Products.GetProductById;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category): ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id) ;
    internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator, ILogger<GetProductByIdHandler> logger) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create product from CreateProductCommandHandler class called @{command}", command);

            var result = await validator.ValidateAsync(command, cancellationToken);
            var error = result.Errors.Select(result => result.ErrorMessage).ToList();
            if (error.Any())
            {
                throw new ValidationException(error.FirstOrDefault());

            }
            var prouct = new Product
            {
               
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile,
                Category = command.Category
            };
            //CreateProductCommandValidator validationRules = await validator.ValidateAsync(prouct);
            session.Store(prouct);
            await session.SaveChangesAsync(cancellationToken);
            // Save the product to the database
            return new CreateProductResult(prouct.Id);
        }
    }
}
