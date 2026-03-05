namespace MoustafaApp.Server.IRepository
{
    public interface CartIRepo: IBaseRepository<Cart>
    {
        Task<IEnumerable<Cart>> GetAllCarts();
        Task<Cart> GetCartById(int id);
        Task<Cart> GetActiveCartByUser(string userId);
    }
}
