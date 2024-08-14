using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Product);
public record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdResult>;

public class GetProductByIdQueryHandler(IDocumentSession session)
    :IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(request.Id, token: cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        return new GetProductByIdResult(product);
    }
}