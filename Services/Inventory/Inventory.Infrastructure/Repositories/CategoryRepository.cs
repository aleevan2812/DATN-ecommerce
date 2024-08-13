using Inventory.Core.Entities;
using Inventory.Core.IRepository;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task UpdateAsync(Category entity)
    {
        _db.Categories.Update(entity);
    }
}