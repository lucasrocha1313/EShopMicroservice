using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.DeleteProductById;


public record DeleteProductByIdResult(bool IsSuccess);
public record DeleteProductByIdCommand(Guid Id): ICommand<DeleteProductByIdResult>;

internal class DeleteProductByIdCommandHandler(IDocumentSession session)
    :ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
{
    public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        session.Delete<Product>(request.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductByIdResult(true);
    }
}