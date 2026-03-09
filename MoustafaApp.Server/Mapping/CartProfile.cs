using MoustafaApp.Server.Dtos.CartDtos;

namespace MoustafaApp.Server.Mapping
{
    public class CartProfile : Profile
    {
        public  CartProfile()
        {
            CreateMap<Cart, CartDto>()
                     .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                     .ForMember(dest => dest.DiscountRate, opt => opt.Ignore())
                     .ForMember(dest => dest.Discount, opt => opt.Ignore())
                     .ForMember(dest => dest.DeliveryFee, opt => opt.Ignore())
                     .ForMember(dest => dest.Total, opt => opt.Ignore());


            CreateMap<CartItem, CartItemDto>()
                     .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                     .ForMember(dest => dest.Photo,opt => opt.MapFrom(src => src.Product.Photo));


            CreateMap<CreateCartDto, Cart>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Items));



            CreateMap<AddItemDto, CartItem>()
                .ForMember(dest => dest.CartItemId, opt => opt.Ignore())
                .ForMember(dest => dest.PriceOfUnit, opt => opt.Ignore())
                .ForMember(dest => dest.Cart, opt => opt.Ignore());

        }


    }
}
