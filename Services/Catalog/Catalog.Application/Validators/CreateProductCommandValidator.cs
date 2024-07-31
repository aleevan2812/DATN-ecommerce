﻿using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(o => o.Summary)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();

            RuleFor(o => o.ImageFile)
           .NotEmpty()
           .WithMessage("{PropertyName} is required")
           .NotNull();

            RuleFor(o => o.Price)
           .NotEmpty()
           .WithMessage("{PropertyName} is required.")
           .GreaterThan(-1)
           .WithMessage("{PropertyName} should not be -ve.");

            RuleFor(o => o.Brands.Id)
           .NotEmpty()
           .WithMessage("{PropertyName} is required.");

            RuleFor(o => o.Brands.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

            RuleFor(o => o.Types.Id)
           .NotEmpty()
           .WithMessage("{PropertyName} is required.");

            RuleFor(o => o.Types.Name)
           .NotEmpty()
           .WithMessage("{PropertyName} is required.");
        }
    }
}