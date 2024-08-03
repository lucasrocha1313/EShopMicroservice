using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.DeleteProductById;

// public record DeleteProductByIdRequest(Guid id);

public record DeleteProductByIdResponse(bool IsSuccess);

public class DeleteProductByIdEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductByIdCommand(id));

            var response = result.Adapt<DeleteProductByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProductById")
        .Produces<DeleteProductByIdResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product by Id")
        .WithDescription("Delete product by id.");
    }
}