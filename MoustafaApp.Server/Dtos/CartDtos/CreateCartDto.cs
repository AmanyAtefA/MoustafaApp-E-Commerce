namespace MoustafaApp.Server.Dtos.CartDtos
{
    public class CreateCartDto
    {
        public string UserId { get; set; }
        public List<AddItemDto> Items { get; set; } = new();
    }
}
