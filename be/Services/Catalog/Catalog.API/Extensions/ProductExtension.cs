using Catalog.API.Models;
using Catalog.API.Responses;

namespace Catalog.API.Extensions;

public static class ProductExtension
{
    public static ProductResponse BuildResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Description = product.Description,
            ImageFile = product.ImageFile,
            Price = product.Price,
        };
    }
}
