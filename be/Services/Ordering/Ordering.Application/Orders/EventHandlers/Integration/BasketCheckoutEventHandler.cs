using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation(
            "Integration Event handled: {IntegrationEvent}",
            context.Message.GetType().Name
        );

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(
            message.FirstName,
            message.LastName,
            message.EmailAddress,
            message.AddressLine,
            message.Country,
            message.State,
            message.ZipCode
        );

        var paymentDto = new PaymentDto(
            message.CardName,
            message.CardNumber,
            message.Expiration,
            message.CVV,
            message.PaymentMethod
        );

        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto
        {
            Id = orderId,
            CustomerId = message.CustomerId,
            OrderName = message.UserName,
            ShippingAddress = addressDto,
            BillingAddress = addressDto,
            Payment = paymentDto,
            Status = OrderStatus.Pending,
            OrderItems = //TODO - get order items from basket
            [
                new OrderItemDto(orderId, new Guid("BB8DB93C-051C-4715-8F5F-972896FB98F4"), 2, 500),
                new OrderItemDto(orderId, new Guid("5E061E54-5665-412E-8407-13000962B4E7"), 1, 400),
            ],
        };

        return new CreateOrderCommand { Order = orderDto };
    }
}
