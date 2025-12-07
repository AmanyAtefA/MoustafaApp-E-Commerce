

namespace moustafapp.Server.IRepository
{
    public interface CategoryIRepo: IBaseRepository<Category>
    {

        Task<IEnumerable<Category>> GetAllCategoriesWithProducts();
        Task<Category> GetCategoryByIdWithProducts( int Id);
    }
}
