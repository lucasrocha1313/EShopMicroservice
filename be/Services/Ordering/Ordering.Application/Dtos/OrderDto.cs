using Ordering.Domain;

namespace Ordering.Application.Dtos;

public class OrderDto
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string OrderName { get; init; }
    public OrderStatus Status { get; init; }
    public AddressDto ShippingAddress { get; init; }
    public AddressDto BillingAddress { get; init; }
    public PaymentDto Payment { get; init; }
    public List<OrderItemDto> OrderItems { get; init; }
}