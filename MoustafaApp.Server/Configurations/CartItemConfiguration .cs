
namespace MoustafaApp.Server.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            
            builder.HasData(new CartItem
            {
                CartItemId = 1,
                CartId = 1,
                ProductId = 1,
                Quantity = 1
            },
            new CartItem
            {
                CartItemId = 2,
                CartId = 2,
                ProductId = 1,
                Quantity = 1
            },
            new CartItem{
                CartItemId = 3,
                CartId = 3,
                ProductId = 1,
                Quantity = 1
            });
        }
    }
}
