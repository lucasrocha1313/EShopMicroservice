using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public class OrderingNotFoundException: NotFoundException
{
    public OrderingNotFoundException(Guid id) : base("Order", id)
    {
    }
}