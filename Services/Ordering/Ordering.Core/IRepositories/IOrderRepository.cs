using Ordering.Core.Entities;

namespace Ordering.Core.IRepositories;

public interface IOrderRepository : IAsyncRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
}