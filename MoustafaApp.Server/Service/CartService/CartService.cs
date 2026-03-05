
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoustafaApp.Server.Attributes;
using MoustafaApp.Server.DomainBusiness.CartBusiness;
using MoustafaApp.Server.Models;
using MoustafaApp.Server.Service.UserService;
using MoustafaApp.Server.Validators;
using moustafapp.Server.GenericOfWork;

namespace MoustafaApp.Server.Service.CartService.CartService
{
    public class CartService : ICartService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CartCalculator _cartCalculator;
        private readonly CartValidator _cartValidator;
        private readonly ICacheService _cache;
        public CartService(IUnitOfWork unitOfWork, IMapper mapper, CartCalculator cartCalculator,
            CartValidator cartValidator, ICurrentUserService currentUser, ICacheService cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartCalculator = cartCalculator;
            _cartValidator = cartValidator;
            _currentUser = currentUser;
            _cache = cache;
        }


        private string UserId  => _currentUser.UserId;

        private async Task RemoveCartCache(string userId)
        {
            var cacheKey = $"cart:{userId}";
            await _cache.RemoveAsync(cacheKey);
        }


        private async Task<CartDto> BuildAndCacheCart(Cart cart)
        {
            _cartValidator.Validate(cart);

            var summary = _cartCalculator.Calculate(cart);

            var dto = _mapper.Map<CartDto>(cart);

            dto.Subtotal = summary.Subtotal;
            dto.DiscountRate = summary.DiscountRate;
            dto.Discount = summary.Discount;
            dto.DeliveryFee = summary.DeliveryFee;
            dto.Total = summary.Total;

            await _cache.SetAsync($"cart:{UserId}", dto, TimeSpan.FromMinutes(10));

            return dto;
        }



        public async Task<CartDto?> GetCart()
        {
            var cacheKey = $"cart:{UserId}";
            var cached = await _cache.GetAsync<CartDto>(cacheKey);
            if (cached != null) return cached;

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId);
            if (cart == null) return null;

            return await BuildAndCacheCart(cart);
        }



        public async Task<CartDto?> GetCartById(int id)
        {
            var cart = await _unitOfWork.Carts.GetCartById(id);
            if (cart == null) return null;

            return await BuildAndCacheCart(cart);
        }

        public async Task<CartDto> CreateCart()
        {
            var existingCart = await _unitOfWork.Carts.GetActiveCartByUser(UserId );

            if (existingCart != null)
                return await GetCart();

            var cart = new Cart
            {
                UserId  = UserId ,
                Status = CartStatusEnum.Active
            };

            await _unitOfWork.Carts.AddAsync(cart);
            await  _unitOfWork.SaveChangesAsync();

            return await GetCart();
        }



        public async Task<CartDto?> AddItemToCart(int productId, int quantity)
        {
            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId );

            if (cart == null)
                return null;

            var product = await _unitOfWork.Products.GetTById(productId);

            if (product == null)
                throw new InvalidOperationException("Product not found.");

            var existingItem = cart.CartItems
                .FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
                existingItem.Quantity += quantity;
            else
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    PriceOfUnit = product.Price
                });

            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);

            
            // 
            //var dto = BuildAndCacheCart(cart);

            //try
            //{
            //    await _cache.SetAsync(key, dto, TimeSpan.FromMinutes(10));
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Redis failed");
            //}

            //return dto;
        }


        public async Task<CartDto?> UpdateQuantity(int productId, int quantity)
        {
            
            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId );
            if (cart == null) return null;

            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) return null;

            if (quantity <= 0)
                cart.CartItems.Remove(item);
            else
                item.Quantity = quantity;


            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);
        }


        public async Task<CartDto?> RemoveItem( int productId)
        {

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId );

            if (cart == null)
                return null;

            var item = cart.CartItems
                .FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
                cart.CartItems.Remove(item);

            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);
        }



        private void ValidateCoupon(Coupon coupon, DateTime now)
        {
            if (!coupon.IsActive)
                throw new InvalidOperationException("Coupon is not active.");

            if (coupon.ExpiryDate <= now)
                throw new InvalidOperationException("Coupon has expired.");
        }


        public async Task<CartDto?> ApplyCoupon( int couponId)
        {

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId );

            if (cart == null)
                return null;

            var coupon = await _unitOfWork.Coupons.GetTById(couponId);

            if (coupon == null)
                throw new InvalidOperationException("Coupon not found.");

            if (cart.CouponId == couponId)
                throw new InvalidOperationException("This coupon is already applied.");

            ValidateCoupon(coupon, DateTime.UtcNow);

            cart.CouponId = couponId;

            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);
        }


        public async Task<CartDto?> ClearCart()
        {
           
            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId );

            if (cart == null)
                return null;

            cart.CartItems.Clear();

            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);
        }



        public async Task<CartDto?> RemoveCoupon()
        {
           
            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId );
            if (cart == null) return null;

            cart.CouponId = null;

            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);
        }


        public async Task<bool> DeleteCart()
        {
            
            var cart = await _unitOfWork.Carts
                .GetFirstOrDefaultAsync(c =>
                    c.UserId  == UserId  &&
                    c.Status == CartStatusEnum.Active);

            if (cart == null) return false;

            _unitOfWork.Carts.Delete(cart);
            await _unitOfWork.SaveChangesAsync();

            await RemoveCartCache(UserId);
            return true;
        }
    }
}
