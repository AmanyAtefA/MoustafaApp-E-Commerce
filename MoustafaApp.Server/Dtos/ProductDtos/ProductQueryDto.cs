using MoustafaApp.Server.Dtos.ProductDtos;

public class ProductQueryDto
{
  
    // Filters
    public int? SizeId { get; set; }
    public int? ColorId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    // Sorting
    public string? SortBy { get; set; }       // price | createdAt
    public string? SortDirection { get; set; } // asc | desc

    // Pagination
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 8;


    // Class Enum Product
    public ProductPreset Preset { get; set; } = ProductPreset.None;


}
