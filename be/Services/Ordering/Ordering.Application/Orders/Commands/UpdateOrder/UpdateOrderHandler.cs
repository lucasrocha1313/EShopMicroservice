using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Application.Orders.Utils;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.Order.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
        
        if (order == null)
        {
            throw new OrderingNotFoundException(request.Order.Id);
        }
        
        OrderUtils.UpdateOrderWithNewValues(order, request.Order);

        if (order != null) dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }
}