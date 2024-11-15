using Ordering.Domain;

namespace Ordering.Application.Dtos;

public class OrderDto
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public required string OrderName { get; init; }
    public OrderStatus Status { get; init; }
    public required AddressDto ShippingAddress { get; init; }
    public required AddressDto BillingAddress { get; init; }
    public required PaymentDto Payment { get; init; }
    public required List<OrderItemDto> OrderItems { get; init; }
}