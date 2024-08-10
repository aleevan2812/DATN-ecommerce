using Microsoft.AspNetCore.Http;

namespace Inventory.Core.Dtos.ProductImage;

public class ProductImageCreateDTO
{
    public IFormFile ProductImage { get; set; }
    public Guid ProductId { get; set; }
}