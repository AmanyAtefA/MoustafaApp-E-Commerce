namespace MoustafaApp.Server.Dtos.CartDtos
{
    public class CreateCartDto
    {
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }

        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
