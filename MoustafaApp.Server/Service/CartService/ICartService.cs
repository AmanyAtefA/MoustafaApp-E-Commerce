using MoustafaApp.Server.Dtos.CartDtos;
using MoustafaApp.Server.Dtos.OrderDtos;

namespace MoustafaApp.Server.Service.CartService.CartService
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserId();

        Task<CartDto?> GetCartById(int id);

        Task<CartDto?> AddItemToCart(AddItemDto request);

        Task<CartDto?> RemoveItem(int cartItemId);

        Task<CartDto?> ApplyCoupon(string code);

        Task<CartDto?> ClearCart();
        Task<CartDto?> UpdateQuantity(UpdateQuantityItemDto dto);

        Task<CartDto?> RemoveCoupon();
        Task<bool> DeleteCart();

        
    }
}
