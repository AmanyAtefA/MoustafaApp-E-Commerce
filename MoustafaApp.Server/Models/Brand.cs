namespace MoustafaApp.Server.Models
{
    public class Brand
    {

        [Key]
        public int BrandId { get; set; }

        [MaxLength(50)]
        public string BrandName { get; set; } = null!;
        public string? PhotoBrand { get; set; }


        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
