namespace MoustafaApp.Server.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
