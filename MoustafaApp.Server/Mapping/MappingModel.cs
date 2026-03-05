using MoustafaApp.Server.Dtos.BrandDtos;
using MoustafaApp.Server.Dtos.CartDtos;
using MoustafaApp.Server.Dtos.CategoryDtos;
using MoustafaApp.Server.Dtos.ProductDtos;
using MoustafaApp.Server.Dtos.Review;
using MoustafaApp.Server.Dtos.ReviewDtos;
using MoustafaApp.Server.Dtos.UserDtos;

namespace moustafapp.Server.Mapping
{
    public class MappingModel : AutoMapper.Profile
    {
        public MappingModel()
        {
    
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Size, SizetDto>().ReverseMap();


            CreateMap<Department, DepartmentDto>();
            CreateMap<Department, DepartmentWithProductDto>();


            
            CreateMap<Review, ReviewDto>()
             .ForMember( dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
             .ForMember(dest => dest.UserName,opt => opt.MapFrom(src => src.User.UserName))
             .ForMember(dest => dest.FullName,opt => opt.MapFrom(src => src.User.FullName));

            CreateMap<CreateReviewDto, Review>();
            CreateMap<Review, ReviewDto>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName));

            CreateMap<UpdateReviewDto, Review>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

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
               .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes))
               .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));


            CreateMap<UpdateProductDto, Product>()
              .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));
            

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();

            CreateMap<ProductColor, ProductColorDto>().ReverseMap();


            CreateMap<ProductSize, ProductSizeDto>()
                 .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.Size.SizeId))
                 .ForMember(dest => dest.SizeName, opt => opt.MapFrom(src => src.Size.SizeName));



           


        }
    }
}
