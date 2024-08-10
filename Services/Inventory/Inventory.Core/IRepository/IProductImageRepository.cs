using Inventory.Core.Entities;

namespace Inventory.Core.IRepository;

public interface IProductImageRepository : IRepository<ProductImage>
{
    Task UpdateAsync(ProductImage entity);
}