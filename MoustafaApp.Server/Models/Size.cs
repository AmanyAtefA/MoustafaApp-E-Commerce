namespace MoustafaApp.Server.Models
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }

        [MaxLength(20)]
        public string SizeName { get; set; } = null!;

        public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
    }
}
