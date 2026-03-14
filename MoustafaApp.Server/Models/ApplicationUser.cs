namespace MoustafaApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
