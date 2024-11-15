using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Orders.Utils;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        //Create Order entity from command object
        //Save Order entity to database
        //Return CreateOrderResult object

        var order = OrderUtils.CreateNewOrder(request.Order);
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }
}