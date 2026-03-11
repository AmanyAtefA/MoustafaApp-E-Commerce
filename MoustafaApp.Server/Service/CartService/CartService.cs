
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using moustafaapp.Server.GenericOfWork;
using MoustafaApp.Server.Attributes;
using MoustafaApp.Server.DomainBusiness.CartBusiness;
using MoustafaApp.Server.Dtos.CartDtos;
using MoustafaApp.Server.Models;
using MoustafaApp.Server.Service.UserService;
using MoustafaApp.Server.Validators;

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



        public async Task<CartDto?> GetCartByUserId()
        {
            var cacheKey = $"cart:{UserId}";
            var cached = await _cache.GetAsync<CartDto>(cacheKey);
            if (cached != null) return cached;

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId);

            if (cart == null)
                return null;

            return await BuildAndCacheCart(cart);
        }



        public async Task<CartDto?> GetCartById(int id)
        {
            var cart = await _unitOfWork.Carts.GetCartById(id);

            if (cart == null)
                throw new InvalidOperationException("Cart not found.");

            return await BuildAndCacheCart(cart);
        }

        private async Task<Cart> GetOrCreateCartEntity()
        {
            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId);

            if (cart != null)
                return cart;

            cart = new Cart
            {
                UserId = UserId,
                Status = CartStatusEnum.Active,
                CartItems = new List<CartItem>()
            };

            await _unitOfWork.Carts.AddAsync(cart);
            await _unitOfWork.SaveChangesAsync();

            return cart;
        }

        public async Task<CartDto?> AddItemToCart(AddItemDto request)
        {
            
            var cart = await GetOrCreateCartEntity();

            var product = await _unitOfWork.Products.GetTById(request.ProductId);

            if (product == null)
                throw new InvalidOperationException("Product not found.");

            var existingItem = cart.CartItems

                   .FirstOrDefault(i =>
                       i.ProductId == request.ProductId &&
                       i.SizeId == request.SizeId &&
                       i.ColorId == request.ColorId
                   );


            if (existingItem != null)
                existingItem.Quantity += request.Quantity;
            else
            { 
                var cartItem = _mapper.Map<CartItem>(request);

                 cartItem.CartId = cart.CartId;
                 cartItem.PriceOfUnit = product.Price;

                await _unitOfWork.CartItems.AddAsync(cartItem);
            }

        
             await _unitOfWork.SaveChangesAsync();

    
            return await BuildAndCacheCart(cart);
          
        }


        public async Task<CartDto?> UpdateQuantity(UpdateQuantityItemDto dto)
        {

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId);

            if (cart == null)
                throw new InvalidOperationException("Cart not found.");


            var item = cart.CartItems.FirstOrDefault(i => i.CartItemId == dto.CartItemId);

            if (item == null) return null;

            if (dto.Quantity <= 0)
                cart.CartItems.Remove(item);
            else
                item.Quantity = dto.Quantity;


            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);
        }


        public async Task<CartDto?> RemoveItem(int cartItemId)
        {

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId);

            if (cart == null)
                throw new InvalidOperationException("Cart not found.");

            var item = cart.CartItems.FirstOrDefault(i => i.CartItemId == cartItemId);

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


        public async Task<CartDto?> ApplyCoupon(string code)
        {
            var cart = await GetOrCreateCartEntity();

            var coupon = await _unitOfWork.Coupons
                .GetFirstOrDefaultAsync(c => c.Code == code);

            if (coupon == null)
                throw new InvalidOperationException("Invalid coupon.");

            ValidateCoupon(coupon, DateTime.UtcNow);

            cart.CouponId = coupon.CouponId;

            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);
        }

        public async Task<CartDto?> ClearCart()
        {

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId);

            if (cart == null)
                throw new InvalidOperationException("Cart not found.");

            cart.CartItems.Clear();

            await _unitOfWork.SaveChangesAsync();

            return await BuildAndCacheCart(cart);
        }



        public async Task<CartDto?> RemoveCoupon()
        {

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(UserId);

            if (cart == null)
                throw new InvalidOperationException("Cart not found.");

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
