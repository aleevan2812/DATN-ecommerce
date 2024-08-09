using MediatR;

namespace Ordering.Application.Commands;

public record UpdateOrderCommand(
    string? Id,
    string? FullName,
    string? EmailAddress,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode
) : IRequest<Unit>;