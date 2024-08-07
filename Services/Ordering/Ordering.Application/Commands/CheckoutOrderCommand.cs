using MediatR;

namespace Ordering.Application.Commands;

public record CheckoutOrderCommand(
    string? UserName,
    decimal? TotalPrice,
    string? FullName,
    string? EmailAddress,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode,
    int? PaymentMethod,
    IEnumerable<CheckoutOrderItem> Items
) : IRequest<string>;

public record CheckoutOrderItem(
    string? ProductId,
    string? ProductName,
    int? Quantity,
    string? ImageUrl,
    decimal? Price
    );