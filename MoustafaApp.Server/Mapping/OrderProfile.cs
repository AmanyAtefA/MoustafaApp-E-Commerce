
using MoustafaApp.Server.Dtos.OrderDtos;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());


        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.PaymentStatus,
            opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
            .ForMember(dest => dest.ShippingStatus,
            opt => opt.MapFrom(src => src.ShippingStatus.ToString()));


        CreateMap<OrderItem, OrderItemDto>();



    }
}