
using MoustafaApp.Server.Dtos.ProductDtos;

namespace moustafaapp.Server.IRepository
{
    public interface ProductIRepo : IBaseRepository<Product>
    {
        Task<IEnumerable<ProductDto>> GetAllProductsWithDetails();
        Task<ProductDto?> GetProductByIdWithDetails(int id);
        Task<PagedResult<ProductDto>> GetProductWithFiltersAsync(ProductFilterQueryDto query);
        Task<PagedResult<ProductDto>> GetAllProductsNewArrivalsAsync(int page, int pageSize);
    }
}

