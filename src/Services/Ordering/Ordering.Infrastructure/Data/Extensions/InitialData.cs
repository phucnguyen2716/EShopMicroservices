namespace Ordering.API.Infrastructure.Data;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
    new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("169ad36c-44af-4fcc-aaf5-5ab5cd0b59dc")),"mehmet","memhet@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("1b94bd14-76f8-447a-90a2-1ac66a470f3e")),"john","john@gmail.com") 
    };
    public static IEnumerable<Product> Products =>
    new List<Product>
    {
        Product.Create(ProductId.Of(Guid.NewGuid()),"IPhone X",500),
        Product.Create(ProductId.Of(Guid.NewGuid()),"Samsung 10",400), 
        Product.Create(ProductId.Of(Guid.NewGuid()),"Huawei Plus",650),
        Product.Create(ProductId.Of(Guid.NewGuid()),"Xiaomi Mi",400), 
    };
    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("mehmet","ozkaya","mehmet@gmail.com","Bahcelievler No:4","Turkey","Istanbul","38050");
            var address2 = Address.Of("john","doe","john@gmail.com","Broadway No:1","England","Nottingham","08050");

            var payment1 = Payment.Of("mehmet","5555555555554444","12/28","355",1);
            var payment2 = Payment.Of("john","8885555555554444","06/30","222",2);
            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("169ad36c-44af-4fcc-aaf5-5ab5cd0b59dc")),
                OrderName.Of("OR001"),
                shippingAddress: address1,
                billingAddress: address1,
                payment: payment1
            );
             var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("1b94bd14-76f8-447a-90a2-1ac66a470f3e")),
                OrderName.Of("OR002"),
                shippingAddress: address2,
                billingAddress: address2,
                payment: payment2
            );
            return new List<Order> { order1, order2 };
        }
    }
    
}