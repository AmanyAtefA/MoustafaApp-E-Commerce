



namespace moustafapp.Server.Repository
{
    public class ProductRepo : BaseRepository<Product>, ProductIRepo
    {

        public ProductRepo(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetAllProductsWithDetails()
        {
            var Products = await _context.Products
                  .Include(p => p.Images)
                  .Include(p => p.Colors)
                  .Include(p => p.Sizes)
                  .ThenInclude(ps => ps.Size)
                  .ToListAsync();


            return (Products);
        }




        public async Task<Product> GetProductyByIdWithDetails(int id)
        {

             var Product = await _context.Products
                  .Include(p => p.Images)
                  .Include(p => p.Colors)
                  .Include(p => p.Sizes)
                  .ThenInclude(ps => ps.Size)
                  .FirstOrDefaultAsync();

          
            return (Product);
        }

    }
}
