namespace MoustafaApp.Server.DomainBusiness.CartBusiness
{
    public class CartSummary
    {
        public decimal Subtotal { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal Discount { get; set; }
        public decimal UserDiscount { get; set; }
        public decimal CouponDiscount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
    }
}
