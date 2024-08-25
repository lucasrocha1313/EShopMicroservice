namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; }
    public string Color { get; set; } = default!;
    public decimal Price { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;
}