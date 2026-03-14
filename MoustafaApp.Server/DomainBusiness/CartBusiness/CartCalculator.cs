using MoustafaApp.Server.Attributes;
using MoustafaApp.Server.Models;

namespace MoustafaApp.Server.DomainBusiness.CartBusiness
{
    public class CartCalculator
    {
        public CartSummary Calculate(Cart cart)
        {
            var subtotal = cart.CartItems
                .Sum(i => i.Quantity * i.PriceOfUnit);

            decimal userDiscount = 0;
            decimal couponDiscount = 0;

            // خصم المستخدم
            if (cart.UserId != null)
            {
                userDiscount = subtotal * 0.20m;
            }

            // خصم الكوبون
            if (cart.Coupon != null &&
                cart.Coupon.IsActive &&
                cart.Coupon.ExpiryDate > DateTime.UtcNow)
            {
                if (cart.Coupon.CouponType == CouponTypeEnum.Percentage)
                {
                    couponDiscount = subtotal * cart.Coupon.Value / 100m;
                }
                else if (cart.Coupon.CouponType == CouponTypeEnum.FixedAmount)
                {
                    couponDiscount = cart.Coupon.Value;
                }

                couponDiscount = Math.Min(couponDiscount, subtotal);
            }


            var discount = userDiscount + couponDiscount;

            // منع الخصم أن يتجاوز subtotal
            discount = Math.Min(discount, subtotal);

            decimal discountRate = subtotal == 0
                ? 0
                : discount / subtotal;

            var deliveryFee = subtotal >= 500 ? 0 :
                              cart.UserId != null ? 10 : 15;

            var total = subtotal - discount + deliveryFee;

            return new CartSummary
            {
                Subtotal = subtotal,
                DiscountRate = discountRate,
                Discount = discount,
                UserDiscount = userDiscount,
                CouponDiscount = couponDiscount,
                DeliveryFee = deliveryFee,
                Total = total
            };
        }
    }
}