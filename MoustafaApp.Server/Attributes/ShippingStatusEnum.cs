namespace MoustafaApp.Server.Attributes
{
    public enum ShippingStatusEnum
    {
        Pending = 1,      // لسه متجهزش
        Processing = 2,  // بيتحضر
        Shipped = 3,     // خرج للشحن
        Delivered = 4,   // اتسلم
        Returned = 5,    // رجع
        Cancelled = 6    // اتلغى
    }
}
