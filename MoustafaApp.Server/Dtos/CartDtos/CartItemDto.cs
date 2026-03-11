namespace MoustafaApp.Server.Dtos.CartDtos
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal PriceOfUnit { get; set; }
        public string Photo { get; set; }= null!;
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }


    }
}