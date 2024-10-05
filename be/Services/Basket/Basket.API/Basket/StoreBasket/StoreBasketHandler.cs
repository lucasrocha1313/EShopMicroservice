using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketResult(string UserName);
public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;



public class StoreBasketCommandHandler(IBasketRepository repository, DiscountService.DiscountServiceClient discountService)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = request.Cart;
        await DeductDiscount(cart, cancellationToken);
        
        await repository.StoreBasket(cart, cancellationToken);
        return new StoreBasketResult(cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountService.GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}