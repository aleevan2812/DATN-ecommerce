using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(o => o.Id)
            .NotEmpty()
            .WithMessage("{Id} is required")
            .NotNull();

            RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage("{Name} is required")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{Name} must not exceed 100 characters");

            RuleFor(o => o.Summary)
            .NotEmpty()
            .WithMessage("{Summary} is required")
            .NotNull();

            RuleFor(o => o.ImageFile)
           .NotEmpty()
           .WithMessage("{ImageFile} is required")
           .NotNull();

            RuleFor(o => o.Price)
           .NotEmpty()
           .WithMessage("{Price} is required.")
           .GreaterThan(-1)
           .WithMessage("{Price} should not be -ve.");

            RuleFor(o => o.Brands.Id)
           .NotEmpty()
           .WithMessage("{Brands.Id} is required.");

            RuleFor(o => o.Brands.Name)
            .NotEmpty()
            .WithMessage("{Brands.Id} is required.");

            RuleFor(o => o.Types.Id)
           .NotEmpty()
           .WithMessage("{Brands.Id} is required.");

            RuleFor(o => o.Types.Name)
           .NotEmpty()
           .WithMessage("{Brands.Id} is required.");
        }
    }
}