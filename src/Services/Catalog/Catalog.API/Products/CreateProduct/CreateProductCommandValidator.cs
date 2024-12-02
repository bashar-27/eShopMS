namespace Catalog.API.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
      {
        public CreateProductCommandValidator()
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(product => product.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(product => product.ImageFile).NotEmpty().WithMessage("Image is required");
            RuleFor(product => product.Price).GreaterThan(0).WithMessage("Price is required and should be greater than 0");

        }
    }
}
