using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);
public record GetProductsQuery(int? PageNumber, int? PageSize): IQuery<GetProductsResult>;

internal class GetProductsQueryHandler(IDocumentSession session)
    :IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, token: cancellationToken);
        
        return new GetProductsResult(products);
    }
}