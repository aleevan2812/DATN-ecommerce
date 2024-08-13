namespace Inventory.Core.Dtos.Product;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Seller { get; set; }
    public Guid CategoryId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Author { get; set; }

    public string? ISBN { get; set; }

    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public int SoldQuantity { get; set; }

    //public List<ProductImageDTO>? ProductImages { get; set; } = new List<ProductImageDTO>();
    public List<string>? ImageUrls { get; set; } = new List<string>();
}