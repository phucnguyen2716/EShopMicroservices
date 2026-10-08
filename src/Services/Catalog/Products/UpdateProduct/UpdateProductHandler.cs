
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name,List<string> Category,string Description,string ImageFile,decimal Price) 
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(Product Product);

internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductsHandler.Handle call with {@Query}",command);
        var product= await session.LoadAsync<Product>(command.Id,cancellationToken);
        
        if (product is null) throw new ProductNotFoundException();

        product.Name = command.Name;
        product.Description = command.Description;
        product.Category=command.Category;
        product.ImageFile=command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(product);
    }
}