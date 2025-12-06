namespace MoustafaApp.Server.Dtos
{
    public class CreateReviewDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Rating { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? DatePosted { get; set; } 

    }
}
