namespace MoustafaApp.Server.Dtos
{
    public class DepartmentWithProductDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
