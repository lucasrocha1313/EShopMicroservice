using FluentValidation;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.Application.Orders.Commands.Validators;

public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is required"); 
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName is required");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
    }
}