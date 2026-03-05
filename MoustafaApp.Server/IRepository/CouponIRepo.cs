namespace MoustafaApp.Server.IRepository
{
    public interface CouponIRepo : IBaseRepository<Coupon>
    {
        Task<Coupon?> GetCouponByCode(string code);

    }

}
