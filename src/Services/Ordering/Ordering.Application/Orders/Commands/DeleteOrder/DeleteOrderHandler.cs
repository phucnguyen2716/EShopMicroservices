
namespace Ordering.API.Application.Orders.Commands.DeletOrder;

public class DeleteOrderHandler (IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = command.OrderId;
        var order = await dbContext.Orders.FindAsync([orderId],cancellationToken: cancellationToken);
        if(order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }
        dbContext.Orders.Remove(order);
        await dbContext.SavingChangeAsync(cancellationToken);
        return new DeleteOrderResult(true);
    }
}