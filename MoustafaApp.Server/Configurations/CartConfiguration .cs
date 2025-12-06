

namespace MoustafaApp.Server.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
        
            builder.HasData(new Cart
            {
                CartId = 1,
                Total = 1500,
                CreatedAt = new DateTime(2025, 11, 30)
            },
            new Cart
            {
                CartId = 2,
                Total = 1000,
                CreatedAt = new DateTime(2025, 11, 30)
            },
            new Cart
            {
                CartId = 3,
                Total = 400,
                CreatedAt = new DateTime(2025, 11, 30)
            });
        }
    }
}
