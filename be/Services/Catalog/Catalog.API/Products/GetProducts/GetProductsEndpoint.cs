using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts;


//TODO: Why not use PaginationRequest from BuildingBlocks.Pagination?
public record GetProductRequest(int? PageNumber, int? PageSize);

//TODO: Should have an DTO or ViewModel for the response
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters]GetProductRequest request,ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
                
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get all products.");
        
    }
}