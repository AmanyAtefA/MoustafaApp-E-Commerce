using MoustafaApp.Server.Dtos.BrandDtos;

namespace MoustafaApp.Server.Repository
{
    public class BrandRepo : BaseRepository<Brand>, BrandIRepo
    {
        private readonly IMapper _mapper;

        public BrandRepo(AppDbContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrands()
        {
            return await _context.Brands
                .AsNoTracking()
                .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BrandDto?> GetBrandById(int id)
        {
            return await _context.Brands
                .AsNoTracking()
                .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.BrandId == id);
        }

    }
}
