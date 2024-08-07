using MediatR;

namespace Ordering.Application.Commands;

public class UpdateOrderCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public string? Status { get; set; }

    public int? PaymentMethod { get; set; }
}