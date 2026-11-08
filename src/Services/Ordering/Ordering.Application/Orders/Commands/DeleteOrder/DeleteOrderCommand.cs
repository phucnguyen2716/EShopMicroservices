namespace Ordering.API.Application.Orders.Commands.DeletOrder;

public record DeleteOrderCommand (Guid OrderId) : ICommand <DeleteOrderResult>;

public record DeleteOrderResult (bool isSucess);

public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderValidator()
    {
        RuleFor(o=>o.OrderId).NotEmpty().WithMessage("OrderId is required");
    }
}