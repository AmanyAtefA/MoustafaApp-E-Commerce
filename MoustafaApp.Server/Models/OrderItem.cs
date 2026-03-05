public class OrderItem
{
    public int OrderItemId { get; private set; }

    public int ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public int OrderId { get; private set; }
    public Order Order { get; private set; } = null!;

    private OrderItem() { } // EF

    internal OrderItem(int productId,
                       string productName,
                       decimal price,
                       int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
}