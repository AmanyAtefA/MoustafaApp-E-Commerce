public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int? Discount { get; set; }
    public decimal? Rating { get; set; }
    public int Stock { get; set; }
    public string? Photo { get; set; }



    public int? BrandId { get; set; }
    public string? BrandName { get; set; }

    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }


    public List<ProductImageDto> Images { get; set; } = new List<ProductImageDto>();
    public List<ProductColorDto> Colors { get; set; } = new List<ProductColorDto>();
    public List<ProductSizeDto> Sizes { get; set; } = new List<ProductSizeDto>();
}
