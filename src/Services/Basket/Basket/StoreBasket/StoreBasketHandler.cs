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

internal class StoreBasketHandler (IBasketRepository repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.StoreBasket(command.Cart,cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
}