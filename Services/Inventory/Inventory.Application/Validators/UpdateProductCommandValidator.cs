using FluentValidation;
using Inventory.Application.Commands;

namespace Inventory.Application.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ISBN)
            .NotEmpty().NotNull().WithMessage("ISBN is required.")
            .Length(10, 13).WithMessage("ISBN must be either 10 or 13 characters long.");

        RuleFor(x => x.Title)
            .NotEmpty().NotNull().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Author)
            .NotEmpty().NotNull().WithMessage("Author is required.")
            .MaximumLength(100).WithMessage("Author name must not exceed 100 characters.");

        RuleFor(x => x.Price)
            .NotNull().NotNull().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be at least 0.");
    }
}