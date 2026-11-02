namespace Ordering.API.Infrastructure.Data;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
    new List<Customer>
    {
        Customer.Create(CustomerId.Of(Guid.NewGuid()),"mehmet","memhet@gmail.com"),
        Customer.Create(CustomerId.Of(Guid.NewGuid()),"john","john@gmail.com") 
    };
    public static IEnumerable<Product> Products =>
    new List<Product>
    {
        Product.Create(ProductId.Of(Guid.NewGuid()),"IPhone X",500),
        Product.Create(ProductId.Of(Guid.NewGuid()),"Samsung 10",400), 
        Product.Create(ProductId.Of(Guid.NewGuid()),"Huawei Plus",650),
        Product.Create(ProductId.Of(Guid.NewGuid()),"Xiaomi Mi",400), 
    };
}