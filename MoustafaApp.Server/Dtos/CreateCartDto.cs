namespace MoustafaApp.Server.Dtos
{
    public class CreateCartDto
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }

        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
