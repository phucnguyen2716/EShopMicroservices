namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
       
        //Handle white space by using %20
        string decoded = Uri.UnescapeDataString(query.Category);
        var products= await session.Query<Product>()
            .Where(x=>x.Category.Contains(decoded))
            .ToListAsync(cancellationToken);

        if (products is null)
        {
            throw new NotFoundException(
                $"Products in category '{query.Category}' not found."
            );
        }
        return new GetProductByCategoryResult(products);
    }
}