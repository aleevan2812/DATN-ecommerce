using Basket.Core.Entities;

namespace Basket.Core.IRepositories;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName);

    Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);

    Task DeleteBasket(string userName);
}