using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;



public record CreateOrderResult(Guid OrderId);

public record CreateOrderCommand: ICommand<CreateOrderResult>
{
    public required OrderDto Order { get; init; }
}