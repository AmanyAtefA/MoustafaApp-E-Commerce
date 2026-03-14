public class Address
{
    public int AddressId { get; set; }

    public string UserId { get; set; } = null!;

    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string? Notes { get; set; }

    public ApplicationUser User { get;set; } = null!;
    public ICollection<Order> Orders { get;set; } = new List<Order>();
   


}