public class CouponRepo : BaseRepository<Coupon>, CouponIRepo
{
    public CouponRepo(AppDbContext context) : base(context)
    {
    }

    public async Task<Coupon?> GetCouponByCode(string code)
    {
        return await _context.Coupons
            .FirstOrDefaultAsync(c => c.Code == code);
    }


}