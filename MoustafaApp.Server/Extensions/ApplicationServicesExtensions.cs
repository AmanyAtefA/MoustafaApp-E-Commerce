using MoustafaApp.Server.DomainBusiness.CartBusiness;
using MoustafaApp.Server.Service.CartService.CartService;
using MoustafaApp.Server.Service.OrderService;
using MoustafaApp.Server.Service.ProductService;
using MoustafaApp.Server.Service.UserService;
using MoustafaApp.Server.Services.Common;
using MoustafaApp.Server.Validators;

namespace MoustafaApp.Server.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IImageService, ImageService>();


            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<IOrderService, OrderService>();


            services.AddScoped<ProductIRepo, ProductRepo>();
            services.AddScoped<CategoryIRepo, CategoryRepo>();
            services.AddScoped<CartIRepo, CartRepo>();
            services.AddScoped<DepartmentIRepo, DepartmentRepo>();
            services.AddScoped<CouponIRepo, CouponRepo>();
            services.AddScoped<ReviewIRepo, ReviewRepo>();
            services.AddScoped<OrderIRepo, OrderRepo>();
            services.AddScoped<BrandIRepo, BrandRepo>();


            services.AddScoped<CartCalculator>();
            services.AddScoped<CartValidator>();


            return services;
        }
    }
}
