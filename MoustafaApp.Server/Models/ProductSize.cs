
namespace MoustafaApp.Server.Models
{
    public class ProductSize
    {
        [ForeignKey("Size")]
        public int SizeId { get; set; }
        public Size Size { get; set; } = null!;


        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }

}
