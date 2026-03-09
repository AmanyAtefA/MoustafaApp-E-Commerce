

using Microsoft.EntityFrameworkCore;
using MoustafaApp.Server.Attributes;

namespace MoustafaApp.Server.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {

            builder.Property(c => c.Status)
                   .HasConversion<string>();

            builder.HasData(new Cart
            {
                CartId = 1,
                Status = CartStatusEnum.Active,
                CreatedAt = new DateTime(2025, 11, 30)
            },
            new Cart
            {
                CartId = 2,
                Status = CartStatusEnum.Active,
                CreatedAt = new DateTime(2025, 11, 30)
            },
            new Cart
            {
                CartId = 3,
                Status = CartStatusEnum.Active,
                CreatedAt = new DateTime(2025, 11, 30)
            });
        }
    }
}
