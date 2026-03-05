using MoustafaApp.Server.Attributes;
using MoustafaApp.Server.DomainBusiness.OrderBusiness;

public class Order
{
    public int Id { get; private set; }

    public string UserId { get; private set; } = null!;
    public ApplicationUser User { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public decimal Subtotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal DeliveryFee { get; private set; }
    public decimal TotalAmount { get; private set; }

    public PaymentStatusEnum PaymentStatus { get; private set; } = PaymentStatusEnum.Pending;
    public string? PaymentIntentId { get; private set; }
    public DateTime? PaidAt { get; private set; }

    public ShippingStatusEnum ShippingStatus { get; private set; } = ShippingStatusEnum.Pending;

    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    public OrderAddress ShippingAddress { get; private set; } = null!;

    public bool IsCompleted =>
        PaymentStatus == PaymentStatusEnum.Paid &&
        ShippingStatus == ShippingStatusEnum.Delivered;

    private Order() { } // EF

    public Order(
    string userId,
    decimal subtotal,
    decimal discount,
    decimal deliveryFee,
    decimal total,
    OrderAddress address)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId is required.");

        UserId = userId;
        Subtotal = subtotal;
        Discount = discount;
        DeliveryFee = deliveryFee;
        TotalAmount = total;
        ShippingAddress = address ?? throw new ArgumentNullException(nameof(address));
    }

    public void AddItem(
        int productId,
        string productName,
        decimal price,
        int quantity)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero.");

        OrderItems.Add(new OrderItem(
            productId,
            productName,
            price,
            quantity
        ));
    }
}