using Basket.Application.Commands;
using FluentValidation;

namespace Basket.Application.Validators;

public class CreateShoppingCartCommandValidator : AbstractValidator<CreateShoppingCartCommand>
{
    public CreateShoppingCartCommandValidator()
    {
        RuleFor(o => o.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .NotNull().WithMessage("Username cannot be null.")
            .Must(username => !string.IsNullOrWhiteSpace(username)).WithMessage("Username cannot be empty or consist only of whitespace.");
    }
}