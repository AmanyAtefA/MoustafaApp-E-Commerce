
namespace moustafapp.Server.IRepository
{
    public interface ProductIRepo : IBaseRepository<Product>
    {

        Task<IEnumerable<Product>> GetAllProductsWithDetails();
        Task<Product> GetProductyByIdWithDetails(int id);

    }
}
