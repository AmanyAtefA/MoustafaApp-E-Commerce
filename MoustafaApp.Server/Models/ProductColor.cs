namespace MoustafaApp.Server.Models
{
    public class ProductColor
    {
        [Key]
        public int ColorId { get; set; }

        [MaxLength(20)]
        public string ColorName { get; set; } = null!;


        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
    }

}
