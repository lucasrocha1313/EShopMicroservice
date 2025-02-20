using BuildingBlocks.Pagination;
using Carter;
using Catalog.API.Extensions;
using Catalog.API.Responses;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(PaginatedResult<ProductResponse> ProductsPaginated);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products",
                async ([AsParameters] PaginationRequest request, ISender sender) =>
                {
                    var result = await sender.Send(
                        new GetProductsQuery(request.PageNumber, request.PageSize)
                    );

                    var response = result.BuildProductResponse();

                    return Results.Ok(response);
                }
            )
            .WithName("GetProducts")
            .Produces<GetProductsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get all products.");
    }
}
