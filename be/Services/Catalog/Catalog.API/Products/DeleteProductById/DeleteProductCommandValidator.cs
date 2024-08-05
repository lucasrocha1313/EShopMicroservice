using FluentValidation;

namespace Catalog.API.Products.DeleteProductById;

public class DeleteProductCommandValidator: AbstractValidator<DeleteProductByIdCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
    }
}