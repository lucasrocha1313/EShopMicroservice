using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrderByCostumer;

public record GetOrderByCostumerQuery(Guid CostumerId) : IQuery<GetOrderByCostumerResult>;

public record GetOrderByCostumerResult(IEnumerable<OrderDto> Orders);