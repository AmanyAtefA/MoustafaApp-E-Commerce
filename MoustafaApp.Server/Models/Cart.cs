

namespace MoustafaApp.Server.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [ForeignKey("UserId")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }


        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }

}
