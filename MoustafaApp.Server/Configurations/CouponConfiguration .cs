using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoustafaApp.Server.Models;
using MoustafaApp.Server.Attributes;

namespace MoustafaApp.Server.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasData(
                new Coupon
                {
                    CouponId = 1,
                    Code = "SALE20",
                    CouponType = CouponTypeEnum.Percentage,
                    Value = 20,
                    ExpiryDate = new DateTime(2026, 12, 31),
                    IsActive = true
                }
            );
        }
    }
}