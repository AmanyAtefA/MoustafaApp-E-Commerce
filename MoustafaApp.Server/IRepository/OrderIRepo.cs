namespace MoustafaApp.Server.IRepository
{
    public interface OrderIRepo : IBaseRepository<Order>
    {

        Task<Order?> GetOrderById(int orderId);
        Task<Order?> GetOrderByUserId(string UserId);
    }
}
