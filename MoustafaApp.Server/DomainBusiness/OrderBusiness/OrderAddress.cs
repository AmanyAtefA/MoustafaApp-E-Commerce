namespace MoustafaApp.Server.DomainBusiness.OrderBusiness
{
    public class OrderAddress
    {
        public string FullName { get; }
        public string PhoneNumber { get; }
        public string City { get; }
        public string Street { get; }
        public string? Notes { get; }

        private OrderAddress() { }
        public OrderAddress(string fullName,string phoneNumber,string city,string street,string? notes)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            Notes = notes;
        }
    }
}
