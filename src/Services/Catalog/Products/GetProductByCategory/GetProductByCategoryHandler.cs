
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryHandler(IDocumentSession session, ILogger logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle call with {@Query}",query);

        var products= await session.Query<Product>()
            .Where(x=>x.Category.Contains(query.Category))
            .ToListAsync();
            
        if(products is null)
        {
            throw new ProductNotFoundException();
        }
        return new GetProductByCategoryResult(products);
    }
}