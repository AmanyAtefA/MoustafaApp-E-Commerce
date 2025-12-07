namespace MoustafaApp.Server.Dtos.CategoryDtos
{
    public class CategoryWithProducDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
