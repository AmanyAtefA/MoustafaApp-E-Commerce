using MoustafaApp.Server.Dtos.OrderDtos;

namespace MoustafaApp.Server.Service.OrderService
{
    public interface IOrderService
    {
       Task<OrderDto?> GetOrderById(int orderId);
        Task<OrderDto?> GetOrderByUserId();


    }
}
