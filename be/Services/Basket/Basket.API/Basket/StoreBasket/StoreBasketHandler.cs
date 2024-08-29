using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketResult(string UserName);
public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;



public class StoreBasketCommandHandler: ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = request.Cart;
        
        //TODO: Store Basket in DB
        //TODO: Update cache
        return new StoreBasketResult("swn");
    }
}