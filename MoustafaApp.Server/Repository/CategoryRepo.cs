



namespace moustafapp.Server.Repository
{
    public class CategoryRepo : BaseRepository<Category>, CategoryIRepo
    {
      
        public CategoryRepo(AppDbContext context) : base(context)
        {
            
        } 

        public async Task<IEnumerable<Category>> GetAllCategoriesWithProducts()
        {
            var Categories = await _context.Categories
                .Include(x => x.Products).ThenInclude(p => p.Images)
                .Include(x => x.Products).ThenInclude(p => p.Colors)
                .Include(x => x.Products).ThenInclude(p => p.Sizes).ThenInclude(s => s.Size)  
                .ToListAsync();

            return (Categories);
        }




        public async Task<Category> GetCategoryByIdWithProducts(int id)
        {

            var Category = await _context.Categories
                .Where(x => x.CategoryId == id)
                .Include(x => x.Products).ThenInclude(p => p.Images)
                .Include(x => x.Products).ThenInclude(p => p.Colors)
                .Include(x => x.Products).ThenInclude(p => p.Sizes).ThenInclude(s => s.Size)
                .FirstOrDefaultAsync();


            return (Category);
        }


    }
}
