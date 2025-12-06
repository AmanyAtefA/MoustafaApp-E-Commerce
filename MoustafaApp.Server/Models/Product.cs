
namespace MoustafaApp.Server.Models
{
    public class Product
    {

        [Key]
        public int ProductId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int? Discount { get; set; }
        public decimal? Rating { get; set; }
        public int Stock { get; set; }
        public string? Photo { get; set; }


        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }


        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }


        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        


        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductColor> Colors { get; set; } = new List<ProductColor>();
        public ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<CartItem> CartItem { get; set; } = new List<CartItem>();
    }

}
