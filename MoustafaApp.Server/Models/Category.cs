namespace MoustafaApp.Server.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [ MaxLength(100)]
        public string CategoryName { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
