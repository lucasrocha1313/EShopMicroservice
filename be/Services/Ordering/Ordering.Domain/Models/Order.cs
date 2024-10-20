using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Order: Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    
    public CustomerId CustomerId { get; set; }
    public OrderName OrderName { get; set; } = default!;

    public Address ShippingAddress { get; set; } = default!;
    public Address BillingAddress { get; set; } = default!;
    
    public Payment Payment { get; set; } = default!;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    
    
    public decimal TotalPrice => _orderItems.Sum(item => item.Price * item.Quantity);
}