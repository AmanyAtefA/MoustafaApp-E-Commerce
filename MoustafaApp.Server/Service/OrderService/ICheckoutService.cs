
using MoustafaApp.Server.Dtos.OrderDtos;

namespace MoustafaApp.Server.Service.OrderService
{
    public interface ICheckoutService
    {
        Task<int> CheckoutAsync(AddressDto dto);
    }
}
