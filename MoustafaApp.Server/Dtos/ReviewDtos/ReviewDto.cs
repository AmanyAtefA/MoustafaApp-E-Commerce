public class ReviewDto
{
    public int ReviewId { get; set; }
    public decimal Rating { get; set; }
    public string? ReviewText { get; set; }
    public DateTime DatePosted { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }

    public string? UserId { get; set; }
    public string? UserName { get; set; }

    public string? FullName { get; set; }
   
}
