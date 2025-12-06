namespace MoustafaApp.Server.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageUrl { get; set; } = null!;


        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


    }

}
