
namespace MoustafaApp.Server.Configurations
{
    public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.HasKey(ps => new { ps.ProductId, ps.SizeId });

            builder.HasData(

            new ProductSize { ProductId = 1, SizeId = 1 },
            new ProductSize { ProductId = 1, SizeId = 2 },
            new ProductSize { ProductId = 1, SizeId = 3 },

            new ProductSize { ProductId = 2, SizeId = 1 },
            new ProductSize { ProductId = 2, SizeId = 2 },
            new ProductSize { ProductId = 2, SizeId = 3 },

            new ProductSize { ProductId = 3, SizeId = 2 },
            new ProductSize { ProductId = 3, SizeId = 3 },
            new ProductSize { ProductId = 3, SizeId = 4 },

            new ProductSize { ProductId = 4, SizeId = 2 },
            new ProductSize { ProductId = 4, SizeId = 3 },
            new ProductSize { ProductId = 4, SizeId = 4 }
            );
        }
    }
}
