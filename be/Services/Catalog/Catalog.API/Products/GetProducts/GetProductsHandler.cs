using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);
public record GetProductsQuery: IQuery<GetProductsResult>;

internal class GetProductsQueryHandler(IDocumentSession session)
    :IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(token: cancellationToken);
        return new GetProductsResult(products);
    }
}