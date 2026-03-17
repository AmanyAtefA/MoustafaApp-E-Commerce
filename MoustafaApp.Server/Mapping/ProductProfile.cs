using MoustafaApp.Server.Dtos.ProductDtos;

namespace MoustafaApp.Server.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()

                   .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                   .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors))
                   .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes))
                   .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                   .ForMember(dest => dest.BrandName,
                        opt => opt.MapFrom(src => src.Brand != null ? src.Brand.BrandName : null))

                   .ForMember(dest => dest.CategoryName,
                        opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null))

                   .ForMember(dest => dest.DepartmentName,
                        opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : null));


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
