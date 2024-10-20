using Ordering.Domain.Abstractions;
using Ordering.Domain.Exceptions;
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

    public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            OrderStatus = OrderStatus.Pending
        };
        
        //TODO: Add domain events

        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus orderStatus)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = orderStatus;
        
        //TODO: Add domain events
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
        
        var itemExists = _orderItems.Any(item => item.ProductId == productId);
        
        if (itemExists)
            throw new DomainException("Product already exists in the order");
        
        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }
    
    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
        if (orderItem is not null)
            _orderItems.Remove(orderItem);
    }
    
    public void Clear()
    {
        _orderItems.Clear();
    }
    
    public void IncreaseQuantity(ProductId productId, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        
        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
        if (orderItem is not null)
            orderItem.Quantity += quantity;
        else
            throw new DomainException("Product not found in the order");
    }
    
    public void DecreaseQuantity(ProductId productId, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        
        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
        if (orderItem is not null)
        {
            orderItem.Quantity -= quantity;
            if (orderItem.Quantity <= 0)
                _orderItems.Remove(orderItem);
        }
        else
        {
            throw new DomainException("Product not found in the order");
        }
    }
}