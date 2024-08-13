using FluentValidation;
using Inventory.Application.Commands;

namespace Inventory.Application.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Category ID is required.").NotNull();
    }
}