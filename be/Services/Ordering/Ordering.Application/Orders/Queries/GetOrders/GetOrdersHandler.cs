using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .OrderBy(o => o.OrderName.Value)
            .Skip(request.PaginationRequest.PageIndex * request.PaginationRequest.PageSize)
            .Take(request.PaginationRequest.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);
        
        var ordersResult = orders.ToOrderDtoList();

        var paginatedResult = new PaginatedResult<OrderDto>(
            pageIndex: request.PaginationRequest.PageIndex,
            pageSize: request.PaginationRequest.PageSize,
            count: await dbContext.Orders.LongCountAsync(cancellationToken: cancellationToken),
            data: ordersResult
        );
        
        return new GetOrdersResult(paginatedResult);
    }
}