
using MoustafaApp.Server.Dtos.ProductDtos;

namespace moustafapp.Server.IRepository
{
    public interface ProductIRepo : IBaseRepository<Product>
    {
        Task<IEnumerable<ProductDto>> GetAllProductsWithDetails();
        Task<ProductDto?> GetProductByIdWithDetails(int id);
        Task<PagedResult<ProductDto>> GetProductsAsync(ProductQueryDto query);
        Task<PagedResult<ProductDto>> GetAllProductsNewArrivalsAsync(int page, int pageSize);
    }
}

