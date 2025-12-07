


using MoustafaApp.Server.Dtos.CartDtos;
using MoustafaApp.Server.Dtos.CategoryDtos;
using MoustafaApp.Server.Dtos.ProductDtos;

namespace moustafapp.Server.Mapping
{
    public class MappingModel : AutoMapper.Profile
    {
        public MappingModel()
        {
    
            CreateMap<Brand, BrandDto>().ReverseMap();


            CreateMap<Department, DepartmentDto>();
            CreateMap<Department, DepartmentWithProductDto>();


            CreateMap<CreateReviewDto, Review>().ReverseMap();



            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryWithProducDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<CreateCategoryDto, Category>()
             .ForMember(dest => dest.CategoryId, opt => opt.Ignore());


            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors))
               .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes));


            CreateMap<UpdateProductDto, Product>()
              .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));
            

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();

            CreateMap<ProductColor, ProductColorDto>().ReverseMap();


            CreateMap<ProductSize, ProductSizeDto>()
                 .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.Size.SizeId))
                 .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size.SizeName));



            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

           
            CreateMap<CreateCartDto, Cart>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Items));

            
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Product.Photo));

            
            CreateMap<CreateCartItemDto, CartItem>()
                .ForMember(dest => dest.CartItemId, opt => opt.Ignore())
                .ForMember(dest => dest.PriceOfUnit, opt => opt.Ignore()) 
                .ForMember(dest => dest.Cart, opt => opt.Ignore());




        }
    }
}
