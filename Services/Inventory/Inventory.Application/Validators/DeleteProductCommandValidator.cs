using FluentValidation;
using Inventory.Application.Commands;

namespace Inventory.Application.Validators;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Category ID is required.");
    }
}