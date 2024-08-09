namespace Ordering.Application.Responses;

public class OrderResponse
{
    public string? Id { get; protected set; }
    public string? UserName { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? TotalDiscount { get; set; }
    public string? FullName { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public int? PaymentMethod { get; set; }

    public string? Status { get; set; } = "Pending";

    public string? PaymentIntentId { get; set; }
    public string? StripeSessionId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }

    public IEnumerable<OrderResponseItem>? Items { get; set; }
}

public class OrderResponseItem
{
    public string? Id { get; set; }

    public string? OrderId { get; set; }

    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
}