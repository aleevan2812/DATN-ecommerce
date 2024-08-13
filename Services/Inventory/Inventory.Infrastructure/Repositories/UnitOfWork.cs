using Inventory.Core.IRepository;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;

namespace Alee_BookEcommerceAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
    }

    public ICategoryRepository Category { get; }
    public IProductRepository Product { get; }

    public async Task<int> SaveAsync()
    {
        var result = await _db.SaveChangesAsync();
        return result;
    }
}