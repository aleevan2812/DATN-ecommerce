namespace EventBus.Messages.Events;

public class ShoppingCartItem
{
    public int? Quantity { get; set; }
    public string? ImageFile { get; set; }
    public decimal Price { get; set; }
    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
}

public class ItemsBasketCheckoutEvent : BaseIntegrationEvent
{
    public string? UserName { get; set; }
    public List<ShoppingCartItem>? Items { get; set; }
}