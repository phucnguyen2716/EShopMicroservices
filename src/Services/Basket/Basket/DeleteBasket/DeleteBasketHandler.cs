
using FluentValidation;

namespace Basket.API.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x=>x.UserName).NotEmpty().WithMessage("User Namee is required");
    }
}

internal class DeleteBasketHandler (IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(request.UserName,cancellationToken);
       return new DeleteBasketResult(true);
    }
}