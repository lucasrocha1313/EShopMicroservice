using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return from order in orders
            let shippingAddress = BuildAddressDto(order.ShippingAddress)
            let billingAddress = BuildAddressDto(order.BillingAddress)
            let payment = BuildPaymentDto(order.Payment)
            select new OrderDto
            {
                Id = order.Id.Value,
                CustomerId = order.CustomerId.Value,
                OrderName = order.OrderName.Value,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = order.OrderStatus,
                OrderItems = BuildOrderItemDtos(order.OrderItems)
            };
    }
    
    public static Order ToOrder(this OrderDto orderDto)
    {
        var shippingAddress = BuildAddress(orderDto.ShippingAddress);
        var billingAddress = BuildAddress(orderDto.BillingAddress);
        var payment = BuildPayment(orderDto.Payment);

        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: payment
        );

        foreach (var item in orderDto.OrderItems)
        {
            newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }

        return newOrder;
    }
    
    public static void UpdateWithNewValues(this Order order, OrderDto requestOrder)
    {
        var updatedShippingAddress = BuildAddress(requestOrder.ShippingAddress);
        var updatedBillingAddress = BuildAddress(requestOrder.BillingAddress);
        var updatedPayment = BuildPayment(requestOrder.Payment);

        order.Update(OrderName.Of(requestOrder.OrderName), updatedShippingAddress, updatedBillingAddress,
            updatedPayment, requestOrder.Status);
    }
    
    private static Payment BuildPayment(PaymentDto payment)
    {
        return Payment.Of(payment.CardName, payment.CardNumber,
            payment.Expiration, payment.Cvv, payment.PaymentMethod);
    }

    private static Address BuildAddress(AddressDto address)
    {
        var shippingAddress = Address.Of(address.FirstName, address.LastName,
            address.EmailAddress, address.AddressLine, address.Country,
            address.State, address.ZipCode);
        return shippingAddress;
    }

    private static List<OrderItemDto> BuildOrderItemDtos(IEnumerable<OrderItem> items)
    {
        return items.Select(item => new OrderItemDto(
            item.Id.Value,
            item.ProductId.Value,
            item.Quantity,
            item.Price
        )).ToList();
    }

    private static PaymentDto BuildPaymentDto(Payment payment)
    {
        var paymentDto = new PaymentDto(
            payment.CardName ?? string.Empty,
            payment.CardNumber,
            payment.Expiration,
            payment.Cvv,
            payment.PaymentMethod
        );
        return paymentDto;
    }

    private static AddressDto BuildAddressDto(Address address)
    {
        var shippingAddress = new AddressDto(
            address.FirstName,
            address.LastName,
            address.EmailAddress ?? string.Empty,
            address.AddressLine,
            address.Country,
            address.State,
            address.ZipCode
        );
        return shippingAddress;
    }
}