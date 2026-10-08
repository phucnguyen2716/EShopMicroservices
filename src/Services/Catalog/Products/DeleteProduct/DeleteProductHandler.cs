namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSucess);

internal class DeleteProductHander(IDocumentSession session, ILogger<DeleteProductResult> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle call with {@Query}",request);
        var product = await session.LoadAsync<Product>(request.Id,cancellationToken);
        if (product is null)    throw new ProductNotFoundException();
        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}