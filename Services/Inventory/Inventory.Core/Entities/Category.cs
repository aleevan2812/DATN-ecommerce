namespace Inventory.Core.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int DisplayOrder { get; set; }
}