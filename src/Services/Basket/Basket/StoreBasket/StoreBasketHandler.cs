using Discount.Grpc;
using FluentValidation;

namespace Basket.API.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketValidator
    : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x=>x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x=>x.Cart.UserName).NotEmpty().WithMessage("UserName is required");

    }
}

internal class StoreBasketHandler (IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        //TO DO: communicate with Discount.Grp and cacaculate the latest price of product
        await DeductDiscount(command.Cart,cancellationToken);
        // Store basket in database(use Marten upsert - if exist = update, if not exist create a new basket)
        await repository.StoreBasket(command.Cart,cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach(var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest{ProductName = item.ProductName},cancellationToken:cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}