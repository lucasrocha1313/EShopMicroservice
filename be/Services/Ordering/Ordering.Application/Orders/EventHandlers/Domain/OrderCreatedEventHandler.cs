using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(
    IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger
) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var isFeatureEnabled = await featureManager.IsEnabledAsync("OrderFulfillment");
        if (!isFeatureEnabled)
        {
            logger.LogInformation("Feature is disabled: {FeatureName}", nameof(OrderCreatedEvent));
            return;
        }

        var orderCreatedEvent = domainEvent.Order.ToOrderDto();
        await publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
    }
}
