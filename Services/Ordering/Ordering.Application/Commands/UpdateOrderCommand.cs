using MediatR;

namespace Ordering.Application.Commands;

public record UpdateOrderCommand(
    string? Id,
    string? UserName,
    string? FullName,
    string? EmailAddress,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode,
    int? PaymentMethod
) : IRequest<Unit>;