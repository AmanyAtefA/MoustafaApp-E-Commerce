namespace MoustafaApp.Server.Dtos
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? PhotoBrand { get; set; }
    }
}
