using Microsoft.AspNetCore.Http;

namespace Inventory.Core.Dtos.Product;

public class ProductUpdateDTO
{
    public Guid Id { get; set; }
    public string Seller { get; set; }
    public string ISBN { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public string Author { get; set; }

    public double? ListPrice { get; set; }

    public double? Price { get; set; }

    public double? Price50 { get; set; }

    public double? Price100 { get; set; }
    public int StockQuantity { get; set; }
    public int SoldQuantity { get; set; }

    public Guid CategoryId { get; set; }

    public List<IFormFile>? ProductImages { get; set; }
}