using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketResult(string UserName);
public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;



public class StoreBasketCommandHandler(IBasketRepository repository): ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = request.Cart;
        
        await repository.StoreBasket(cart, cancellationToken);
        return new StoreBasketResult(cart.UserName);
    }
}