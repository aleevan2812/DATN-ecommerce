using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded.");
        }
    }

    private static IEnumerable<Order> GetOrders()
    {
        return new List<Order>
        {
            new()
            {
                UserName = "rahul",
                FullName = "Rahul",

                EmailAddress = "rahulsahay@eshop.net",
                AddressLine = "Bangalore",
                Country = "India",
                TotalPrice = 200,
                State = "KA",
                ZipCode = "560001",

                Status = "Completed",
                PaymentMethod = 1,
                LastModifiedBy = "Rahul",
                LastModifiedDate = new DateTime(),

                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        ProductId = "pro1",
                        ProductName = "Pro 1",
                        Quantity = 1,
                        ImageUrl = "http://example.com/images/prod001.png",
                        Price = 100
                    },
                    new OrderItem()
                    {
                        ProductId = "pro2",
                        ProductName = "Pro 2",
                        Quantity = 2,
                        ImageUrl = "http://example.com/images/prod001.png",
                        Price = 50
                    }
                }
            }
        };
    }
}