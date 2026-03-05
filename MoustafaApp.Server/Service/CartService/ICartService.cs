namespace MoustafaApp.Server.Service.CartService.CartService
{
    public interface ICartService
    {
        Task<CartDto> GetCart();

        Task<CartDto?> GetCartById(int id);

        Task<CartDto> CreateCart();

        Task<CartDto?> AddItemToCart(int productId, int quantity);

        Task<CartDto?> RemoveItem(int productId);

        Task<CartDto?> ApplyCoupon(int couponId);

        Task<CartDto?> ClearCart();
        Task<CartDto?> UpdateQuantity(int productId, int quantity);

        Task<CartDto?> RemoveCoupon();
        Task<bool> DeleteCart();
    }
}
