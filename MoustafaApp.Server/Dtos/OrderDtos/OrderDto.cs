namespace MoustafaApp.Server.Dtos.OrderDtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = null!;

        public List<OrderItemDto> Items { get; set; } = new();
    }
}
