

namespace MoustafaApp.Server.IRepository
{
    public interface ReviewIRepo : IBaseRepository<Review>
    {
        Task<IEnumerable<ReviewDto>> GetAllReviews();
        Task<IEnumerable<ReviewDto>> GetReviewsByProductId(int id);
        Task<PagedResult<ReviewDto>> GetPagedReviewsByProductId(
        int productId, int pageNumber = 1, int pageSize = 5);
        Task<ReviewStatsDto> GetReviewStatsByProductId(int productId);
        Task CreateReviewAsync(CreateReviewDto dto, string userId);
        Task<bool> UpdateReviewAsync(int reviewId, UpdateReviewDto dto, string userId, bool isAdmin);
        Task<bool> DeleteReviewAsync(int reviewId, string userId, bool isAdmin);
    }
}
