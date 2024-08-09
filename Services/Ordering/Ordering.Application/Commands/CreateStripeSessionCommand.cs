using MediatR;

namespace Ordering.Application.Commands;

public class CreateStripeSessionCommand : IRequest<CreateStripeSessionCommand>
{
    public string? StripeSessionUrl { get; set; }
    public string? StripeSessionId { get; set; }
    public string ApprovedUrl { get; set; }
    public string CancelUrl { get; set; }
    public string OrderId { get; set; }
}