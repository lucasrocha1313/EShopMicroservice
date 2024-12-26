using Basket.API.Data;
using Basket.API.Dtos;
using Basket.API.Exceptions;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto)
    : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandHandler(
    IBasketRepository repository,
    IPublishEndpoint publishEndpoint
) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(
        CheckoutBasketCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var basket = await repository.GetBasket(
                request.BasketCheckoutDto.UserName,
                cancellationToken
            );

            //TODO: Add items to event
            var eventMessage = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            //TODO: what happens if the event is not published or the order creation fails?
            await repository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
        catch (BasketNotFoundException)
        {
            return new CheckoutBasketResult(false);
        }
    }
}
