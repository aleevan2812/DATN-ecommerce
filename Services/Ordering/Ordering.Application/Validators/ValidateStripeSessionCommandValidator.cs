using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;

public class ValidateStripeSessionCommandValidator : AbstractValidator<ValidateStripeSessionCommand>
{
    public ValidateStripeSessionCommandValidator()
    {
        RuleFor(x => x.orderId)
            .NotEmpty().WithMessage("OrderId is required.");
    }
}