using Inventory.Core.Entities;

namespace Inventory.Core.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    Task UpdateAsync(Category entity);
}