

namespace moustafapp.Server.GenericOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        ProductIRepo Products  { get; }
        CategoryIRepo Categories { get; }
        DepartmentIRepo Departments { get; }
        CartIRepo Carts { get; }
        ReviewIRepo Reviews { get; }
        CouponIRepo Coupons { get; }
        IBaseRepository<CartItem> CartItems { get;}
        IBaseRepository<ProductImage> ProductImage { get; }
        IBaseRepository<ApplicationUser> Users { get; }
        IBaseRepository<Brand> Brands { get; }
        IBaseRepository<ProductColor> ProductColors { get; }
        IBaseRepository<ProductSize> ProductSizes { get; }
        IBaseRepository<Size> Sizes { get; }

        IBaseRepository<Order> Orders { get; }
        IBaseRepository<OrderItem> OrderItems { get; }

        Task<int> SaveChangesAsync();
    }
}
