namespace MoustafaApp.Server.Dtos
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public int PriceOfUnit { get; set; }
        public string Photo { get; set; }= null!;

    }
}