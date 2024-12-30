using Ordering.Domain.Abstractions;
using Ordering.Domain.Models;

namespace Ordering.Domain.Events;

public class OrderCreatedEvent : IDomainEvent
{
    public Order Order { get; init; }
}
