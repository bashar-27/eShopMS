
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) :ICommand<StoreBasetResult>;
    public record StoreBasetResult(string userName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }

    }

    public class StoreBasketCommandHandler
        : ICommandHandler<StoreBasketCommand, StoreBasetResult>
    {
        

        public async Task<StoreBasetResult> Handle(StoreBasketCommand command , CancellationToken Cancle)
        {
            ShoppingCart cart = command.Cart;
            return new StoreBasetResult("Bashar");
                }
    }
}
