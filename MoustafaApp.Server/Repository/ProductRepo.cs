using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MoustafaApp.Server.Dtos.ProductDtos;

namespace moustafaapp.Server.Repository
{
    public class ProductRepo : BaseRepository<Product>, ProductIRepo
    {
        private readonly IMapper _mapper;

        public ProductRepo(AppDbContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsWithDetails()
        {
            return await _context.Products
                .AsNoTracking()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ProductDto?> GetProductByIdWithDetails(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }




        private void ValidateQuery(ProductQueryDto dto)
        {
            if (dto.PageNumber <= 0)
                dto.PageNumber = 1;

            if (dto.PageSize <= 0 || dto.PageSize > 100)
                dto.PageSize = 8;

            if (dto.MinPrice.HasValue && dto.MaxPrice.HasValue &&
                dto.MinPrice > dto.MaxPrice)
            {
                throw new ArgumentException("MinPrice cannot be greater than MaxPrice");
            }
        }



        private IQueryable<Product> ApplyPreset(
             IQueryable<Product> query,ProductQueryDto dto)
        {
            return dto.Preset switch
            {
                ProductPreset.NewArrivals =>
                    query.Where(p => p.CreatedAt >= DateTime.UtcNow.AddDays(-30))
                         .OrderByDescending(p => p.CreatedAt),

                ProductPreset.TopRated =>
                     query.Where(p => p.Reviews.Any())
                    .OrderByDescending(p => p.Reviews.Any()
                     ? p.Reviews.Average(r => r.Rating) : 0),

                ProductPreset.BestSeller =>
                    query.OrderByDescending(p => p.Rating),

                _ => query
            };
        }



        private IQueryable<Product> ApplyFilters(
            IQueryable<Product> query, ProductQueryDto dto)
        {
            if (dto.SizeId.HasValue)
                query = query.Where(p =>
                    p.Sizes.Any(s => s.SizeId == dto.SizeId));

            if (dto.ColorId.HasValue)
                query = query.Where(p =>
                    p.Colors.Any(c => c.ColorId == dto.ColorId));

            if (dto.MinPrice.HasValue)
                query = query.Where(p => p.Price >= dto.MinPrice.Value);

            if (dto.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= dto.MaxPrice.Value);

            return query;
        }



        private IQueryable<Product> ApplySorting(
            IQueryable<Product> query, ProductQueryDto dto)

        {
            if (dto.Preset != ProductPreset.None)
                return query; 

            return dto.SortBy?.ToLower() switch
            {
                "price" => dto.SortDirection == "asc"
                    ? query.OrderBy(p => p.Price)
                    : query.OrderByDescending(p => p.Price),

                _ => query.OrderByDescending(p => p.CreatedAt)
            };
        }



        private async Task<PagedResult<ProductDto>> ApplyPagination(
          IQueryable<Product> query, ProductQueryDto dto)

        {
            var totalCount = await query.CountAsync();

            var items = await query
                .AsNoTracking()
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<ProductDto>
            {
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)dto.PageSize),
                Items = items
            };
        }



        public async Task<PagedResult<ProductDto>> GetProductsAsync(ProductQueryDto dto)
        {
            ValidateQuery(dto);

            IQueryable<Product> query = _context.Products.AsQueryable();

            query = ApplyPreset(query, dto);
            query = ApplyFilters(query, dto);
            query = ApplySorting(query, dto);

            return await ApplyPagination(query, dto);
        }



        public async Task<PagedResult<ProductDto>> GetAllProductsNewArrivalsAsync(int page, int pageSize)
        {
            var query = _context.Products
                //.Where(p => p.CreatedAt >= DateTime.UtcNow.AddDays(-30))
                .OrderByDescending(p => p.CreatedAt);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<ProductDto>
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = items
            };
        }
    }
}
