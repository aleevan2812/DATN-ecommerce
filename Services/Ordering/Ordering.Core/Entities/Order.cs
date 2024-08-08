using Ordering.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Core.Entities;

public class Order : EntityBase
{
    public string? UserName { get; set; }
    public decimal? TotalPrice { get; set; } = 0;
    public decimal? TotalDiscount { get; set; } = 0;
    public string? FullName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public int? PaymentMethod { get; set; }

    public string? Status { get; set; } = "Pending";

    public string? PaymentIntentId { get; set; }
    public string? StripeSessionId { get; set; }

    public IEnumerable<OrderItem>? Items { get; set; }
}

public class OrderItem : EntityBase
{
    public string? OrderId { get; set; }

    [ForeignKey("OrderId")]
    public Order? Order { get; set; }

    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}