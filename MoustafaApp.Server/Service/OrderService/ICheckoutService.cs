using MoustafaApp.Server.DomainBusiness.OrderBusiness;

namespace MoustafaApp.Server.Service.OrderService
{
    public interface ICheckoutService
    {
        Task<int> CheckoutAsync(CheckoutRequest request);
    }
}
