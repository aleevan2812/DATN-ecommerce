namespace Basket.Core.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public decimal TotalDiscount { get; set; } = 0;
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}