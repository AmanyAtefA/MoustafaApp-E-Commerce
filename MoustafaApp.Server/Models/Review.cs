
using System.ComponentModel.DataAnnotations.Schema;

namespace MoustafaApp.Server.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public decimal Rating { get; set; }

        [MaxLength(500)]
        public string? ReviewText { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;


        [ForeignKey("Product")]
       
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        [ForeignKey("User")]
        
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }

}
