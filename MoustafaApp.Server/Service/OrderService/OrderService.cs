using MoustafaApp.Server.DomainBusiness.CartBusiness;
using MoustafaApp.Server.Dtos.OrderDtos;
using MoustafaApp.Server.Service.UserService;
using MoustafaApp.Server.Validators;

namespace MoustafaApp.Server.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }


        private string UserId => _currentUser.UserId;
        

        public async Task<OrderDto?> GetOrderById(int orderId)
        {
            var order = await _unitOfWork.Orders.GetOrderById(orderId);

            if (order == null)
                return null;

            return _mapper.Map<OrderDto>(order);
        }


        public async Task<OrderDto?> GetOrderByUserId()
        {
            if (string.IsNullOrEmpty(UserId))
                throw new UnauthorizedAccessException("Login required");

            var order = await _unitOfWork.Orders.GetOrderByUserId(UserId);

            if (order == null)
                return null;

            return _mapper.Map<OrderDto>(order);
        }
    }
}
