

namespace MoustafaApp.Server.Configurations
{
    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
           
            builder.HasData(
                new ProductColor {
                    ColorId = 1,
                    ProductId = 1,
                    ColorName = "Navy"  
                },

                new ProductColor {
                    ColorId = 2,
                    ProductId = 1,
                    ColorName = "Olive" 
                },


                new ProductColor {
                    ColorId = 3,
                    ProductId = 2,
                    ColorName = "Blue"  
                },


                new ProductColor {
                    ColorId = 4,
                    ProductId = 4,
                    ColorName = "Orange"
                },

                new ProductColor {
                    ColorId = 5,
                    ProductId = 1,
                    ColorName = "Black" 
                },

                new ProductColor {
                    ColorId = 6,
                    ProductId = 3,
                    ColorName = "Black" 
                }
            );
        }
    }
}
