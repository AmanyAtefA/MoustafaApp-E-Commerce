namespace MoustafaApp.Server.Attributes
{
    public enum PaymentStatusEnum
    {
        Pending = 1,      // لسه ما اتدفعش
        Paid = 2,         // تم الدفع بنجاح
        Failed = 3,       // الدفع فشل
        Refunded = 4,     // تم استرجاع المبلغ
        Cancelled = 5     // اتلغى قبل الدفع
    }
}
