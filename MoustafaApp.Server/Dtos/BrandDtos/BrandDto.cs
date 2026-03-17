using MoustafaApp.Server.Dtos.ProductDtos;

namespace MoustafaApp.Server.Dtos.BrandDtos
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? PhotoBrand { get; set; }
        public List<ProductDto> Images { get; set; } = new List<ProductDto>();
    }
}
