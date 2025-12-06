

namespace MoustafaApp.Server.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            
            builder.HasData(
                new ProductImage { ImageId = 1, ProductId = 1, ImageUrl = "assets/images/image-D1.png" },
                new ProductImage { ImageId = 2, ProductId = 1, ImageUrl = "assets/images/image-D2.png" },
                new ProductImage { ImageId = 3, ProductId = 1, ImageUrl = "assets/images/image-D3.png" },
               
                     
                new ProductImage { ImageId = 4, ProductId = 2, ImageUrl = "https://example.com/images/skinny-jeans.jpg" },
                new ProductImage { ImageId = 5, ProductId = 2, ImageUrl = "https://example.com/images/skinny-jeans-side.jpg" },
                new ProductImage { ImageId = 6, ProductId = 2, ImageUrl = "https://example.com/images/skinny-jeans-back.jpg" },


                new ProductImage { ImageId = 7, ProductId = 3, ImageUrl = "https://example.com/images/checkered-shirt.jpg" },
                new ProductImage { ImageId = 8, ProductId = 3, ImageUrl = "https://example.com/images/checkered-shirt-side.jpg" },
                new ProductImage { ImageId = 9, ProductId = 3, ImageUrl = "https://example.com/images/checkered-shirt-back.jpg" },


                new ProductImage { ImageId = 10, ProductId = 4, ImageUrl = "https://example.com/images/sleeve-striped.jpg"  },
                new ProductImage { ImageId = 11, ProductId = 4, ImageUrl = "https://example.com/images/sleeve-striped-side.jpg" },
                new ProductImage { ImageId = 12, ProductId = 4, ImageUrl = "https://example.com/images/sleeve-striped-back.jpg" }

                );
        }
    }
}
