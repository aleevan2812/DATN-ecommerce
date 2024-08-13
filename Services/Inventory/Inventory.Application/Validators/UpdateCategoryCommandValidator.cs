﻿using FluentValidation;

namespace Inventory.Application.Commands;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Category ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().NotNull().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(1).WithMessage("Display order must be at least 1.");
    }
}