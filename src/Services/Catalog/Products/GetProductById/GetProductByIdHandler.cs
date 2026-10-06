using BuildingBlock.CQRS;

namespace Catalog.API.GetProdductsById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler (IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().FirstOrDefaultAsync( p => p.Id == request.Id );
        return new GetProductByIdResult(products!);
    }
}