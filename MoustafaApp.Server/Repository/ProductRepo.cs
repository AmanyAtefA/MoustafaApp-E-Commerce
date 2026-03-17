using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MoustafaApp.Server.Dtos.ProductDtos;
using Nest;

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



        private void ValidateQuery(ProductFilterQueryDto dto)
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



        private IQueryable<Product> ApplySearch(
           IQueryable<Product> query, ProductFilterQueryDto dto)

        {
            if (!string.IsNullOrWhiteSpace(dto.Search))
            {
                var search = dto.Search.Trim().ToLower();

                query = query.Where(p =>
                    p.Name.ToLower().Contains(search) ||
                     (p.Category != null &&
                      p.Category.CategoryName.ToLower().Contains(search)) ||

                     (p.Department != null &&
                      p.Department.DepartmentName.ToLower().Contains(search)) ||

                     (p.Brand != null &&
                      p.Brand.BrandName.ToLower().Contains(search)) ||

                    p.Colors.Any(c => c.ColorName.ToLower().Contains(search))||

                    p.Sizes.Any(s => s.Size != null &&
                    s.Size.SizeName.ToLower().Contains(search))
                );                
            }

            return query;
        }


        private IQueryable<Product> ApplyPreset(
                  IQueryable<Product> query,ProductFilterQueryDto dto)
        {
            switch (dto.Preset)
            {
                default:
                case ProductPreset.NewArrivals:
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;

                case ProductPreset.TopRated:
                    query = query.OrderByDescending(p => p.Rating);
                    break;

                //case ProductPreset.BestSeller:
                //    query = query.OrderByDescending(p => p.CartItem.Count);
                //    break;

                case ProductPreset.BestSeller:
                    query = query.OrderBy(p => p.CreatedAt);
                    break;
            }

            return query;
        }



        private IQueryable<Product> ApplyFiltersById(
            IQueryable<Product> query, ProductFilterQueryDto dto)
        {
            if (dto.BrandId.HasValue)
                query = query.Where(p => p.BrandId == dto.BrandId);

            if (dto.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == dto.CategoryId);

            if (dto.DepartmentId.HasValue)
                query = query.Where(p => p.DepartmentId == dto.DepartmentId);

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

            if (dto.OnSale == true)
            {
                query = query.Where(p => p.Discount > 0);
            }
            return query;
        }




        private IQueryable<Product> ApplySorting(
            IQueryable<Product> query, ProductFilterQueryDto dto)

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
          IQueryable<Product> query, ProductFilterQueryDto dto)

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



        public async Task<PagedResult<ProductDto>> GetProductWithFiltersAsync(ProductFilterQueryDto dto)
        {
            ValidateQuery(dto);

            IQueryable<Product> query = _context.Products.AsNoTracking().AsQueryable();

            query = ApplySearch(query, dto);
            query = ApplyFiltersById(query, dto);
            query = ApplyPreset(query, dto);
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
