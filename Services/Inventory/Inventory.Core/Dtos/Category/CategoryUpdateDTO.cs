namespace Inventory.Core.Dtos.Category;

public class CategoryUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public int DisplayOrder { get; set; }
}