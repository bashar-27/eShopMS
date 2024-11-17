
using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) :ICommand<StoreBasetResult>;
    public record StoreBasetResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }

    }

    public class StoreBasketCommandHandler(IBasketRepository basketRepository)
        : ICommandHandler<StoreBasketCommand, StoreBasetResult>
    {
        

        public async Task<StoreBasetResult> Handle(StoreBasketCommand command , CancellationToken Cancle)
        {
            await basketRepository.StoreBasket(command.Cart , Cancle);
            return new StoreBasetResult(command.Cart.UserName);
                }
    }
}
