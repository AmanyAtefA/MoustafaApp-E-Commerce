using MoustafaApp.Server.Attributes;

public class Cart
{
    public int CartId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public int? CouponId { get; set; }
    public Coupon? Coupon { get; set; }

    public CartStatusEnum Status { get; set; } = CartStatusEnum.Active;

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}