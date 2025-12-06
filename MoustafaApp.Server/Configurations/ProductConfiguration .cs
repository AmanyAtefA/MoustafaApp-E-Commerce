
namespace MoustafaApp.Server.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           
            builder.HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "One Life Graphic T-Shirt",
                    Description = "This graphic t-shirt which is perfect for any occasion. Crafted from a soft and breathable fabric, it offers superior comfort and style.",
                    Photo = "assets/images/arr1.png",
                    Price = 260m,
                    OldPrice = 300m,
                    Discount = 13,
                    Rating = 4.5m,
                    Stock = 100,
                    BrandId = 1, 
                    CategoryId = 1, 
                    DepartmentId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Skinny Fit Jeans",
                    Description = "Comfortable skinny fit jeans",
                    Photo = "assets/images/image 2",
                    Price = 240m,
                    OldPrice = 260m,
                    Discount = 8,
                    Rating = 3.5m,
                    Stock = 50,
                    BrandId = 2, 
                    CategoryId = 2, 
                    DepartmentId = 1 
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Checkered Shirt",
                    Description = "Classic checkered shirt",
                    Photo = "assets/images/image3.png",
                    Price = 180m,
                    OldPrice = 0,
                    Discount = 0,
                    Rating = 4.5m,
                    Stock = 80,
                    BrandId = 5, 
                    CategoryId = 3, 
                    DepartmentId = 1, 
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Sleeve Striped T-Shirt",
                    Description = "Striped raglan tee",
                    Photo = "assets/images/arr4.png",
                    Price = 130m,
                    OldPrice = 160m,
                    Discount = 18,
                    Rating = 4.5m,
                    Stock = 70,
                    BrandId = 4, 
                    CategoryId = 1, 
                    DepartmentId = 1
                }
            );
        }
    }
}
