using MoustafaApp.Server.Attributes;

public class Order
{
    public int OrderId { get; set; }

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal DeliveryFee { get;    set; }
    public decimal TotalAmount { get;    set; }
    public int AddressId { get; set; }
    public Address Address { get; set; } = null!;
    public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.Pending;
    public string? PaymentIntentId { get; set; }
    public DateTime? PaidAt { get; set; }

   
    public ShippingStatusEnum ShippingStatus { get; set; } = ShippingStatusEnum.Pending;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public bool IsCompleted =>
        PaymentStatus == PaymentStatusEnum.Paid &&
        ShippingStatus == ShippingStatusEnum.Delivered;

   
}