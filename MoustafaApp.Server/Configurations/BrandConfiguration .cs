
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

namespace MoustafaApp.Server.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
          
            builder.HasData(
                new Brand 
                { BrandId = 1,
                    BrandName = "Versace",
                    PhotoBrand = "assets/images/Versace.png"
                },


                new Brand {
                    BrandId = 2
                    , BrandName = "ZARA",
                    PhotoBrand = "assets/images/Zara.png" 
                },


                new Brand { 
                    BrandId = 3,
                    BrandName = "GUCCI",
                    PhotoBrand = "assets/images/Gucci.png" 
                },

                new Brand { BrandId = 4,
                    BrandName = "PRADA",
                    PhotoBrand = "assets/images/Prada.png" 
                },

                new Brand {
                    BrandId = 5,
                    BrandName = "Calvin Klein",
                    PhotoBrand = "assets/images/CalvinKlein.png" 
                }
            );
        }
    }
}
