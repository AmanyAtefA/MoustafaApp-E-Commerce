
namespace moustafapp.Server.IRepository
{
    public interface ProductIRepo : IBaseRepository<Product>
    {

        Task<IEnumerable<Product>> GetAllProductsWithDetails();
        Task<Product> GetProductyByIdWithDetails(int id);
        Task<PagedResult<ProductDto>> GetAllProductsNewArrivalsAsync(int page, int pageSize);
    }
}
