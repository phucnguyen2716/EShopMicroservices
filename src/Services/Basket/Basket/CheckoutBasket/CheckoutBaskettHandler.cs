
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.CheccoutBasketEndpoints;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto)
    : ICommand <CheckoutBasketResult>;

public record CheckoutBasketResult(bool isSuccess);
public class CheckoutBasketCommandValidator 
    : AbstractValidator <CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator(){
        RuleFor(x=>x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckOutDto can't be null");
        RuleFor(x=>x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CheckoutBasketHandler
    (IBasketRepository repository, IPublishEndpoint publishEvent)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository. GetBasket(command.BasketCheckoutDto.UserName,cancellationToken);
        if (basket is null)
            return new CheckoutBasketResult(false);
        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckOutEvent>();
        eventMessage.TotalPrice = eventMessage.TotalPrice;

        await publishEvent.Publish(eventMessage);
        await repository.DeleteBasket(command.BasketCheckoutDto.UserName,cancellationToken);
        return new CheckoutBasketResult(true);
    }
}