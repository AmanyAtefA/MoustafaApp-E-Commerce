namespace MoustafaApp.Server.Dtos.ReviewDtos
{
    public class UpdateReviewDto
    {
        public decimal Rating { get; set; }

        [MaxLength(500)]
        public string? ReviewText { get; set; }
    }

}
