using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Core.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Seller { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public string Author { get; set; }

    public string ISBN { get; set; }

    public double? ListPrice { get; set; }

    public double? Price { get; set; }

    public double? Price50 { get; set; }

    public double? Price100 { get; set; }
    public int StockQuantity { get; set; }

    public Guid CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }

    public List<ProductImage>? ProductImages { get; set; } = new List<ProductImage>();
}