namespace Inventory.Core.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }

    Task<int> SaveAsync();
}