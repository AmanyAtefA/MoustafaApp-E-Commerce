



using AutoMapper.QueryableExtensions;

namespace moustafapp.Server.Repository
{
    public class ProductRepo : BaseRepository<Product>, ProductIRepo
    {
        private readonly IMapper _mapper;
        public ProductRepo(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
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
                  .FirstOrDefaultAsync(p => p.ProductId == id);

          
            return (Product);
        }



        public async Task<PagedResult<ProductDto>> GetAllProductsNewArrivalsAsync( int page, int pageSize)
        {
            var query = _context.Products
                .AsNoTracking()
                .OrderByDescending(p => p.CreatedAt);

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<ProductDto>
            {
                Items = products,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

    }
}
