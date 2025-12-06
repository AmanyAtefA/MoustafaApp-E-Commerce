using AutoMapper;
using MoustafaApp.Server.IRepository;
using Nest;

namespace MoustafaApp.Server.Repository
{
    public class CartRepo : BaseRepository<Cart>, CartIRepo
    {
        public CartRepo(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            var Carts = await _context.Carts
                .Include(c => c.CartItems)
                  .ThenInclude(p => p.Product)
            .ToListAsync();

           
            return (Carts);
        }


        public async Task<Cart> GetCartById(int id)
        {
            var Cart = await _context.Carts
                .Where(x => x.CartId == id)
                .Include(c => c.CartItems)
                  .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync();

            
            return (Cart);
        }
    }
}
