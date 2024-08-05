namespace Basket.Application.Responses;

public class ShoppingCartResponse
{
    public string UserName { get; set; }
    public decimal TotalDiscount { get; set; } = 0;

    public List<ShoppingCartItemResponse> Items { get; set; }

    public ShoppingCartResponse()
    {
    }

    public ShoppingCartResponse(string userName)
    {
        UserName = userName;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            decimal totalDiscount = TotalDiscount;

            foreach (var item in Items)
            {
                totalPrice += item.Price * item.Quantity;
            }

            return (totalPrice > totalDiscount ? totalPrice - totalDiscount : 0);
        }
    }
}