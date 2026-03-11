namespace MoustafaApp.Server.Dtos.CartDtos
{
    public class AddItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
    }
}
