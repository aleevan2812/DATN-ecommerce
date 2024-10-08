namespace Ordering.Core.Entities;

public record OrderDto
(
    string? UserName,

    string Id,

    //Below Properties are Audit properties
    string? CreatedBy,

    DateTime? CreatedDate,
    string? LastModifiedBy,
    DateTime? LastModifiedDate,

    decimal? TotalPrice,
    string? FullName,
    string? EmailAddress,
    string? PhoneNumber,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode,
    int? PaymentMethod,

    string? Status,

    string? PaymentIntentId,
    string? StripeSessionId,

    IEnumerable<OrderItemDto>? Items
);

public record OrderItemDto(string Id, string? CreatedBy, string CouponCode, DateTime? CreatedDate, string? LastModifiedBy, DateTime? LastModifiedDate, string? OrderId, string? ProductId, string? ProductName, string? ImageUrl, int Quantity, decimal Price);