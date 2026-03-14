namespace MoustafaApp.Server.Dtos.OrderDtos
{
    public class CheckoutDto
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string? Notes { get; set; }
    }
}