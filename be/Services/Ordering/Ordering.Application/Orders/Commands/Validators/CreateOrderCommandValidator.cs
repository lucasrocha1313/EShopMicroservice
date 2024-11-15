using FluentValidation;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.Commands.Validators;

public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull().WithMessage("Order is required");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems is required");
    }
}