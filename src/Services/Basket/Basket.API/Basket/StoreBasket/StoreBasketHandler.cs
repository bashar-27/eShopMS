
using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasetResult>;
    public record StoreBasetResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }

    }

    public class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasetResult>
    {


        public async Task<StoreBasetResult> Handle(StoreBasketCommand command, CancellationToken Cancle)
        {
            //ToDo: Communicate with Discount.Grpc and calculate lastest prices of product
            await DeductDiscount(command.Cart, Cancle);

            await basketRepository.StoreBasket(command.Cart, Cancle);
            return new StoreBasetResult(command.Cart.UserName);
        }
        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProdcutName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}