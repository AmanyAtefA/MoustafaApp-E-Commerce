namespace MoustafaApp.Server.Repository
{
    public class OrderRepo : BaseRepository<Order>, OrderIRepo
    {

        public OrderRepo(AppDbContext context) : base(context)
        {

        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                 .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<Order?> GetOrderByUserId(string userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                 .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.UserId == userId);
        }


    }
}
