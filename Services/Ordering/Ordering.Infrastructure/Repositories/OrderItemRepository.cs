using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.IRepositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories;

public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(OrderContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(string orderId)
    {
        var items = await _dbContext.Items
             .Where(o => o.OrderId == orderId)
             .ToListAsync();
        return items;
    }
}