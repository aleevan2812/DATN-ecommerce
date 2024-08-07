using Ordering.Core.Entities;

namespace Ordering.Core.IRepositories;

public interface IOrderItemRepository : IAsyncRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(string orderId);
}