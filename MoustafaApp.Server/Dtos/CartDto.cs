namespace MoustafaApp.Server.Dtos
{
    public class CartDto
    {
        public int CartId { get; set; }
        public string? UserId { get; set; }
        public string UserName { get; set; } = null!;
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
