using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Application.Extensions;
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
        
        order.UpdateWithNewValues(request.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }
}