using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductResult(bool IsSuccess);

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price)
    : ICommand<UpdateProductResult>;

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("[UpdateProductCommandHandler] - called with {@Command}", request);
        
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        product.Name = request.Name;
        product.Category = request.Category;
        product.Description = request.Description;
        product.ImageFile = request.ImageFile;
        product.Price = request.Price;
        
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}