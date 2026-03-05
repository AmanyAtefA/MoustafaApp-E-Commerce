namespace MoustafaApp.Server.DomainBusiness.OrderBusiness
{
    public class CheckoutRequest
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
