using MoustafaApp.Server.Dtos.CartDtos;

public class CartDto
{
    public int CartId { get; set; }
    public string? UserId { get; set; }
    public string UserName { get; set; } = null!;

    public decimal Subtotal { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal Discount { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal Total { get; set; }

    public int? CouponId { get; set; }

    public List<CartItemDto> CartItems { get; set; } = new();
}