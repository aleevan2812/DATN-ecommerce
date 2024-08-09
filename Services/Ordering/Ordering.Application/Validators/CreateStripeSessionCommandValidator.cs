using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;

public class CreateStripeSessionCommandValidator : AbstractValidator<CreateStripeSessionCommand>
{
    public CreateStripeSessionCommandValidator()
    {
        RuleFor(x => x.StripeSessionUrl)
            .NotEmpty().WithMessage("StripeSessionUrl is required.");

        RuleFor(x => x.StripeSessionId)
            .NotEmpty().WithMessage("StripeSessionId is required.");

        RuleFor(x => x.ApprovedUrl)
            .NotEmpty().WithMessage("ApprovedUrl is required.");

        RuleFor(x => x.CancelUrl)
            .NotEmpty().WithMessage("CancelUrl is required.");

        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required.");
    }
}