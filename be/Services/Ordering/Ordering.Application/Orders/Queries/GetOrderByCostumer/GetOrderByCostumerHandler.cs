using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrderByCostumer;

public class GetOrderByCostumerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByCostumerQuery, GetOrderByCostumerResult>
{
    public async Task<GetOrderByCostumerResult> Handle(GetOrderByCostumerQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Of(request.CostumerId))
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);
        
        var ordersResult = orders.ToOrderDtoList();
        
        return new GetOrderByCostumerResult(ordersResult);
    }
}