using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

// public record DeleteOrderRequest(Guid OrderId);
public record DeleteOrderResponse(bool IsSuccessful);

public class DeleteOrder: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{OrderId}", async (Guid OrderId, ISender sender) =>
            {
                var command = new DeleteOrderCommand(OrderId);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Order")
            .WithDescription("Delete an existing order in the system");
    }
}