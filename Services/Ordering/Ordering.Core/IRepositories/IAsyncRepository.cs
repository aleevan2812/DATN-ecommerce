using Ordering.Core.Common;
using System.Linq.Expressions;

namespace Ordering.Core.IRepositories;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? include = null);

    Task<T> GetByIdAsync(string id);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}