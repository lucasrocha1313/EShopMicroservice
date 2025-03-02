using FluentValidation;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.Application.Orders.Commands.Validators;

public class DeleteOrderCommandValidator: AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
    }
}