using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.AddressId);

        builder.Property(a => a.FullName).IsRequired().HasMaxLength(150);
        builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Street).IsRequired().HasMaxLength(250);
        builder.Property(a => a.Notes).HasMaxLength(500);

        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}