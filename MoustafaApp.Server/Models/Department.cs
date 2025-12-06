namespace MoustafaApp.Server.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [ MaxLength(100)]
        public string DepartmentName { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
