public class UpdateProductDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int? Discount { get; set; }
    public int? Stock { get; set; }
    public IFormFile? Photo { get; set; }


    public int? BrandId { get; set; }
    public int? CategoryId { get; set; }
    public int? DepartmentId { get; set; }



    public List<CreateProductImageDto>? Images { get; set; }
    public List<ProductColorDto>? Colors { get; set; }
    public List<ProductSizeDto>? Sizes { get; set; }
}
