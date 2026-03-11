using AutoMapper;
using MoustafaApp.Server.Attributes;
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
                  .Include(c => c.Coupon)
            .ToListAsync();

           
            return (Carts);
        }


        public async Task<Cart> GetCartById(int id)
        {
            var Cart = await _context.Carts
                .Where(x => x.CartId == id)
                .Include(c => c.Coupon)
                .Include(c => c.CartItems)
                  .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync();

            
            return (Cart);
        }



        public async Task<Cart> GetActiveCartByUser(string userId)
        {
            var cart = await _context.Carts
                                 .Include(c => c.Coupon)
                                 .Include(c => c.CartItems)
                                     .ThenInclude(ci => ci.Product)
                                 .Include(c => c.CartItems)
                                     .ThenInclude(ci => ci.Size)
                                 .Include(c => c.CartItems)
                                     .ThenInclude(ci => ci.Color)
                                 .FirstOrDefaultAsync(
                                     c => c.UserId == userId && 
                                     c.Status == CartStatusEnum.Active);
             return cart;
        }



    }

}
