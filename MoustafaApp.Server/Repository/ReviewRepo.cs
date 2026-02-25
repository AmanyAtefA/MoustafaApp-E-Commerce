


namespace MoustafaApp.Server.Repository
{
    public class ReviewRepo : BaseRepository<Review>, ReviewIRepo
    {

        private readonly IMapper _mapper;

        public ReviewRepo(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return (reviews);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByProductId(int id)
        {
            
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == id)
                .Include(r => r.User)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return (reviews);
        }


        public async Task<PagedResult<ReviewDto>> GetPagedReviewsByProductId(
        int productId, int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User);

            var totalCount = await query.CountAsync();

            var reviews = await query
                .OrderByDescending(r => r.DatePosted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<ReviewDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = reviews
            };

        }

        public async Task<ReviewStatsDto> GetReviewStatsByProductId(int productId)
        {
            return await _context.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .GroupBy(r => r.ProductId)
                .Select(g => new ReviewStatsDto
                {
                    ReviewCount = g.Count(),
                    AverageRating = g.Any() ? Math.Round(g.Average(x => x.Rating), 1) : 0
                })
                .FirstOrDefaultAsync()
                ?? new ReviewStatsDto();
        }

        public async Task CreateReviewAsync(CreateReviewDto dto, string userId)
        {
           
            //var exists = await _context.Reviews
            //    .AnyAsync(r => r.ProductId == dto.ProductId && r.UserId == userId);

            //if (exists)
            //    throw new InvalidOperationException("You already reviewed this product");

            var review = _mapper.Map<Review>(dto);
            review.UserId = userId;
            review.DatePosted = DateTime.UtcNow;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> UpdateReviewAsync(int reviewId,UpdateReviewDto dto,string userId,bool isAdmin)
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
                return false;

            if (!isAdmin && review.UserId != userId)
                throw new UnauthorizedAccessException("Not allowed");

            _mapper.Map(dto, review);
            review.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId,string userId,bool isAdmin)
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
                return false;

            if (!isAdmin && review.UserId != userId)
                throw new UnauthorizedAccessException("Not allowed");

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}