namespace Catalog.API.Products.GetProdductsById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler (IDocumentSession session,ILogger<GetProductByIdHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle call with {@Query}",query);
        var product = await session.LoadAsync<Product>(query.Id,cancellationToken);
        if(product is null)
        {
            throw new ProductNotFoundException();
        }
        return new GetProductByIdResult(product);
    }
}