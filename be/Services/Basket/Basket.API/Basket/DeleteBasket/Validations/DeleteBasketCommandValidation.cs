using FluentValidation;

namespace Basket.API.Basket.DeleteBasket.Validations;

public class DeleteBasketCommandValidation: AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty");
    }
}