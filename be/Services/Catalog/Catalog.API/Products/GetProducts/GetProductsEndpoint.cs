using Carter;
using Catalog.API.Extensions;
using Catalog.API.Responses;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts;

//TODO: Why not use PaginationRequest from BuildingBlocks.Pagination?
public record GetProductRequest(int? PageNumber, int? PageSize);

public record GetProductsResponse(IEnumerable<ProductResponse> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products",
                async ([AsParameters] GetProductRequest request, ISender sender) =>
                {
                    var query = request.Adapt<GetProductsQuery>();

                    var result = await sender.Send(query);

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
