
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace Ordering.API.Application.Orders.EventHandlers;

public class BasketCheckoutEventHandler 
    (ILogger<BasketCheckoutEventHandler> logger, ISender sender )
    : IConsumer<BasketCheckOutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
    {
        logger.LogInformation("Intergration Event handled: {IntergrationEvent}",context.Message.GetType());
        
        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckOutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName,message.LastName,message.EmailAddress!,message.AddressLine,message.Country,message.State,message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName!,message.CardNumber,message.Expiration,message.CVV,message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId,new Guid("1B94BD14-76F8-447A-90A2-1AC66A470F3E"),2,500),
                new OrderItemDto(orderId,new Guid("169AD36C-44AF-4FCC-AAF5-5AB5CD0B59DC"),1,400)
            ]);
        return new CreateOrderCommand(orderDto);
    }
}