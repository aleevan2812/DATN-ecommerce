using MediatR;

namespace Ordering.Application.Commands;

public record CheckoutOrderCommand(
    string UserName,
    string? FullName,
    string? EmailAddress,
    string? PhoneNumber,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode,
    int? PaymentMethod,
    IEnumerable<CheckoutOrderItem> Items
) : IRequest<string>;

public record CheckoutOrderItem(
     string? CouponCode,
    string ProductId,
    string ProductName,
    int Quantity,
    string ImageUrl,
    decimal Price
    );