
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name,List<string> Category,string Description,string ImageFile,decimal Price) 
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(Product Product);

internal class UpdateProductHandler(IDocumentSession session, ILogger logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product= await session.LoadAsync<Product>(request.Id,cancellationToken);
        
        if (product is null) throw new ProductNotFoundException();

        product.Name = request.Name;
        product.Description = request.Description;
        product.Category=request.Category;
        product.ImageFile=request.ImageFile;
        product.Price = request.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(product);
    }
}