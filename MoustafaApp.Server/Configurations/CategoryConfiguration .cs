
namespace MoustafaApp.Server.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            
            builder.HasData(
                new Category { CategoryId = 1, CategoryName = "T-Shirts" },
                new Category { CategoryId = 2, CategoryName = "Jeans" },
                new Category { CategoryId = 3, CategoryName = "Shirts" },
                new Category { CategoryId = 4, CategoryName = "Dresses" },
                new Category { CategoryId = 5, CategoryName = "Shoes" }
            );
        }
    }
}
