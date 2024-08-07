using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.IRepositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orderList = await _dbContext.Orders
            .Where(o => o.UserName == userName)
            .Include(o => o.Items)
            .ToListAsync();
        return orderList;
    }

    public async Task<Order> GetByIdAsync(string id)
    {
        return await _dbContext.Orders
        .Include(o => o.Items)
        .FirstOrDefaultAsync(o => o.Id == id);
    }
}