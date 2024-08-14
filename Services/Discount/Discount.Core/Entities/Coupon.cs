using System.ComponentModel.DataAnnotations;

namespace Discount.Core.Entities;

public class Coupon
{
    [Key]
    public Guid Id { get; set; }

    public string ProductId { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public int Quantity { get; set; }
}