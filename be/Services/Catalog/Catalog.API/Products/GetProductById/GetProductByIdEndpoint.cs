using Carter;
using Catalog.API.Extensions;
using Catalog.API.Models;
using Catalog.API.Responses;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(ProductResponse Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products/{id}",
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByIdQuery(id));
                    var response = new GetProductByIdResponse(result.Product.BuildResponse());

                    return Results.Ok(response);
                }
            )
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Id")
            .WithDescription("Get product by id.");
    }
}
