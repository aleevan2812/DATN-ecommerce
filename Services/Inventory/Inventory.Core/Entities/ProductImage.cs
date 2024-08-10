using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Core.Entities;

public class ProductImage
{
    [Key]
    public Guid Id { get; set; }

    public string ImageUrl { get; set; }
    public string ImagesLocalPath { get; set; }

    [ForeignKey("Product")]
    public Guid ProductId { get; set; }

    public Product Product { get; set; }
}