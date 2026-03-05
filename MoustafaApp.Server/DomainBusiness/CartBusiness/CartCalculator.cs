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

            decimal discount = 0;

            // 2️⃣ حساب الخصم
            if (cart.Coupon != null &&
                cart.Coupon.IsActive &&
                cart.Coupon.ExpiryDate > DateTime.UtcNow)
            {
                if (cart.Coupon.CouponType == CouponTypeEnum.Percentage)
                {
                    discount = subtotal * cart.Coupon.Value;
                }
                else if (cart.Coupon.CouponType == CouponTypeEnum.FixedAmount)
                {
                    discount = cart.Coupon.Value;
                }

                // 🛑 منع الخصم إنه يكون أكبر من السلة
                discount = Math.Min(discount, subtotal);
            }
            else if (cart.UserId != null)
            {
                // خصم 20% للمستخدم المسجل لو مفيش كوبون
                discount = subtotal * 0.20m;
            }

            // 3️⃣ حساب نسبة الخصم (لو محتاجة ترجعيها)
            decimal discountRate = subtotal == 0
                ? 0
                : discount / subtotal;

            // 4️⃣ حساب مصاريف الشحن
            var deliveryFee = subtotal >= 500 ? 0 :
                              cart.UserId != null ? 10 : 15;

            // 5️⃣ حساب الإجمالي
            var total = subtotal - discount + deliveryFee;

            return new CartSummary
            {
                Subtotal = subtotal,
                DiscountRate = discountRate,
                Discount = discount,
                DeliveryFee = deliveryFee,
                Total = total
            };
        }
    }
}