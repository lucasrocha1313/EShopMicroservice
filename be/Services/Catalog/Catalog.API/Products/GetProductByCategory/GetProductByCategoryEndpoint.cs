using Carter;
using Catalog.API.Enums;
using Catalog.API.Extensions;
using Catalog.API.Models;
using Catalog.API.Responses;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<ProductResponse> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products/category/{category}",
                async (CategoryEnum category, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByCategoryQuery(category));
                    var response = result.BuildProductByCategoryResponse();

                    return Results.Ok(response);
                }
            )
            .WithName("GetProductsByCategory")
            .Produces<GetProductByCategoryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products by Category")
            .WithDescription("Get products by category.");
    }
}
