namespace MoustafaApp.Server.Dtos.ReviewDtos
{
    public class ProductReviewsResponseDto
    {
        public ReviewStatsDto Stats { get; set; } = new();
        public PagedResult<ReviewDto> Reviews { get; set; } = null!;
    }

}
