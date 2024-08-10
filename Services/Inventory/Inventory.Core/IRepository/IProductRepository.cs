using Inventory.Core.Entities;
using System.Linq.Expressions;

namespace Inventory.Core.IRepository;

public interface IProductRepository : IRepository<Product>
{
    Task UpdateAsync(Product entity);

    Task<Product> GetAsyncWithProductImages(Expression<Func<Product, bool>> filter = null,
        bool tracked = true,
        string? includeProperties = null);

    Task<List<Product>> GetAllAsyncWithProductImages(Expression<Func<Product, bool>>? filter = null, string? includeProperties = null,
        int pageSize = 0, int pageNumber = 1);
}