using Basket.API.Data;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResult(bool isSuccess);

public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;

public class DeleteBasketHandler(IBasketRepository repository): ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var res = await repository.DeleteBasket(request.UserName, cancellationToken);
        return new DeleteBasketResult(res);
    }
}