using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class OrderItem: Entity<OrderItemId>
{
    public OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
    
    public OrderId OrderId { get; set; }
    public ProductId ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}