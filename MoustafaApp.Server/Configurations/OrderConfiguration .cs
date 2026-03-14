using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderId);

        builder.Property(o => o.Subtotal).HasPrecision(18, 2);
        builder.Property(o => o.Discount).HasPrecision(18, 2);
        builder.Property(o => o.DeliveryFee).HasPrecision(18, 2);
        builder.Property(o => o.TotalAmount).HasPrecision(18, 2);

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Address)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.AddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}