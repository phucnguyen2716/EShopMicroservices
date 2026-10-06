namespace Catalog.API.Exceptions;
    
public class ProductNotFoundException : Exception
{
    public class ProductNotFound : base("Product Not Found");
} 

