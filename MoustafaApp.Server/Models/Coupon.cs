using MoustafaApp.Server.Attributes;

namespace MoustafaApp.Server.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; } = null!;

        public CouponTypeEnum CouponType { get; set; }
        public decimal Value { get; set; }

        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
