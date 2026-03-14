using MoustafaApp.Server.Attributes;
using MoustafaApp.Server.DomainBusiness.CartBusiness;
using MoustafaApp.Server.Dtos.OrderDtos;
using MoustafaApp.Server.Service.UserService;
using MoustafaApp.Server.Validators;


namespace MoustafaApp.Server.Service.OrderService
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly CartCalculator _cartCalculator;
        private readonly CartValidator _cartValidator;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;

        public CheckoutService(
            IUnitOfWork unitOfWork,ICurrentUserService currentUser,
            CartCalculator cartCalculator,CartValidator cartValidator,
            ICacheService cache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _cartCalculator = cartCalculator;
            _cartValidator = cartValidator;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<int> CheckoutAsync(AddressDto dto)
        {
            var userId = _currentUser.UserId;

            var cart = await _unitOfWork.Carts.GetActiveCartByUser(userId);

            if (cart == null || !cart.CartItems.Any())
                throw new InvalidOperationException("Cart is empty.");

            // Validate cart
            _cartValidator.Validate(cart);

            // Calculate totals
            var summary = _cartCalculator.Calculate(cart);

            // Create Address
            var address = _mapper.Map<Address>(dto);
            address.UserId = userId;

            await _unitOfWork.Addresses.AddAsync(address);
            await _unitOfWork.SaveChangesAsync();

            // Create Order
            var order = new Order
            {
                UserId = userId,
                Subtotal = summary.Subtotal,
                Discount = summary.Discount,
                DeliveryFee = summary.DeliveryFee,
                TotalAmount = summary.Total,
                AddressId = address.AddressId
            };

            // Create Order Items
            foreach (var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.PriceOfUnit,
                    Quantity = item.Quantity
                });
            }

            await _unitOfWork.Orders.AddAsync(order);

            // Close cart
            cart.Status = CartStatusEnum.CheckedOut;

            await _unitOfWork.SaveChangesAsync();

            // Clear cache
            await _cache.RemoveAsync($"cart:{userId}");

            return order.OrderId;
        }
    }
}
