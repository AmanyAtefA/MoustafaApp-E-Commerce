namespace MoustafaApp.Server.Dtos.ProductDtos
{
    public class ProductFilterQueryDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;

        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? DepartmentId { get; set; }

        public int? SizeId { get; set; }
        public int? ColorId { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string? Search { get; set; }
        public bool? OnSale { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }

        public ProductPreset Preset { get; set; } = ProductPreset.None;
    }

}
