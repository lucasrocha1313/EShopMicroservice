using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Mapper;

public static class OrderMapper
{
    public static Order CreateNewOrder(OrderDto requestOrder)
    {
        var shippingAddress = Address.Of(requestOrder.ShippingAddress.FirstName, requestOrder.ShippingAddress.LastName,
            requestOrder.ShippingAddress.EmailAddress, requestOrder.ShippingAddress.AddressLine, requestOrder.ShippingAddress.Country,
            requestOrder.ShippingAddress.State, requestOrder.ShippingAddress.ZipCode);
        
        var billingAddress = Address.Of(requestOrder.BillingAddress.FirstName, requestOrder.BillingAddress.LastName,
            requestOrder.BillingAddress.EmailAddress, requestOrder.BillingAddress.AddressLine, requestOrder.BillingAddress.Country,
            requestOrder.BillingAddress.State, requestOrder.BillingAddress.ZipCode);
        
        var payment = Payment.Of(requestOrder.Payment.CardName, requestOrder.Payment.CardNumber,
            requestOrder.Payment.Expiration, requestOrder.Payment.CVV, requestOrder.Payment.PaymentMethod);

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
}