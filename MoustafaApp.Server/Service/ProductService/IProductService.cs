namespace MoustafaApp.Server.Service.ProductService
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto> UpdateAsync(int id, UpdateProductDto dto);
        Task DeleteAsync(int id);
    }
}
