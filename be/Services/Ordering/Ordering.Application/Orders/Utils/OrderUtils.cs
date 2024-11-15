using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Utils;

public static class OrderUtils
{
    public static Order CreateNewOrder(OrderDto requestOrder)
    {
        var shippingAddress = BuildAddress(requestOrder.ShippingAddress);
        
        var billingAddress = BuildAddress(requestOrder.BillingAddress);
        
        var payment = BuildPayment(requestOrder.Payment);

        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(requestOrder.CustomerId),
            orderName: OrderName.Of(requestOrder.OrderName),
            shippingAddress: shippingAddress,
            billingAddress: billingAddress,
            payment: payment
        );
        
        foreach (var item in requestOrder.OrderItems)
        {
            newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }
        
        return newOrder;
    }

    public static void UpdateOrderWithNewValues(Order? order, OrderDto requestOrder)
    {
        if (order == null) return;
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
}