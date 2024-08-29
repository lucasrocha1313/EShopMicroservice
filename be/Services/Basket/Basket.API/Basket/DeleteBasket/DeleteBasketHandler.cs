using BuildingBlocks.CQRS;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResult(bool isSuccess);

public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;

public class DeleteBasketHandler: ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        //TODO: Delete Basket in DB
        return new DeleteBasketResult(true);
    }
}