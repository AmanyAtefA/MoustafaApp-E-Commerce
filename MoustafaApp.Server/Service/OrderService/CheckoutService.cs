using MoustafaApp.Server.Attributes;
using MoustafaApp.Server.DomainBusiness.CartBusiness;
using MoustafaApp.Server.DomainBusiness.OrderBusiness;
using MoustafaApp.Server.Dtos.OrderDtos;
using MoustafaApp.Server.Service.UserService;
using MoustafaApp.Server.Validators;
using StackExchange.Redis;

namespace MoustafaApp.Server.Service.OrderService
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly CartCalculator _cartCalculator;
        private readonly CartValidator _cartValidator;
        private readonly ICacheService _cache;

        public CheckoutService(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUser,
            CartCalculator cartCalculator,
            CartValidator cartValidator,
            ICacheService cache)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _cartCalculator = cartCalculator;
            _cartValidator = cartValidator;
            _cache = cache;
        }

        public async Task<int> CheckoutAsync(CheckoutRequest request)
        {
            var userId = _currentUser.UserId;

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(userId);
            if (cart == null || !cart.CartItems.Any())
                throw new InvalidOperationException("Cart is empty.");

            // Final Validation
            _cartValidator.Validate(cart);

            var summary = _cartCalculator.Calculate(cart);

            // Create Order
            var order = new Order(
                userId,
                summary.Subtotal,
                summary.Discount,
                summary.DeliveryFee,
                summary.Total,
                new OrderAddress(
                    request.FullName,
                    request.PhoneNumber,
                    request.City,
                    request.Street,
                    request.Notes
                )
            );

            // Add Order Items Snapshot
            foreach (var item in cart.CartItems)
            {
                order.AddItem(
                    item.ProductId,
                    item.Product.Name,
                    item.PriceOfUnit,
                    item.Quantity
                );
            }

            await _unitOfWork.Orders.AddAsync(order);

            // Close Cart
            cart.Status = CartStatusEnum.CheckedOut;

            await _unitOfWork.SaveChangesAsync();

            await _cache.RemoveAsync($"cart:{userId}");

            return order.Id;
        }
    }
}
