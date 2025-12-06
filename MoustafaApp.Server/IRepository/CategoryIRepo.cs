

namespace moustafapp.Server.IRepository
{
    public interface CategoryIRepo: IBaseRepository<Category>
    {

        Task<IEnumerable<Category>> GetAllCategoriesWithProductsThenInclude();
        Task<Category> GetCategoryByIdWithProductsThenInclude( int Id);
    }
}
