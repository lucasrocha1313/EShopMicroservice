using Catalog.API.Models;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Responses;

namespace Catalog.API.Extensions;

public static class ResponseExtensions
{
    public static GetProductsResponse BuildProductResponse(this GetProductsResult result)
    {
        var productsResponse = result.Products.Select(x => x.BuildResponse());

        var response = new GetProductsResponse(productsResponse);
        return response;
    }

    public static GetProductByCategoryResponse BuildProductByCategoryResponse(
        this GetProductByCategoryResult result
    )
    {
        var productsResponse = result.Products.Select(x => x.BuildResponse());

        var response = new GetProductByCategoryResponse(productsResponse);
        return response;
    }
}
