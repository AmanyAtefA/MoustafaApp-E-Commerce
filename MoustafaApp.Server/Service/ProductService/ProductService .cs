namespace MoustafaApp.Server.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imgService;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageService imgService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imgService = imgService;
        }

        // ================= CREATE =================
        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.Images ??= new List<ProductImage>();

            if (dto.Photo != null)
                product.Photo = _imgService.Save(dto.Photo);

            if (dto.Images != null)
            {
                foreach (var img in dto.Images)
                {
                    if (img.Photo == null) continue;

                    var url = _imgService.Save(img.Photo);
                    product.Images.Add(new ProductImage { ImageUrl = url });
                }
            }

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        // ================= UPDATE =================
        public async Task<ProductDto> UpdateAsync(int id, UpdateProductDto dto)
        {
            var oldProduct = await _unitOfWork.Products.GetByIdWithIncludes(
                p => p.ProductId == id,
                x => x.Images);

            if (oldProduct == null)
                throw new KeyNotFoundException("Product Not Found");

            _mapper.Map(dto, oldProduct);

            if (dto.Photo != null)
            {
                if (!string.IsNullOrEmpty(oldProduct.Photo))
                    _imgService.Delete(oldProduct.Photo);

                oldProduct.Photo = _imgService.Save(dto.Photo);
            }

            _unitOfWork.Products.Update(oldProduct);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(oldProduct);
        }

        // ================= DELETE =================
        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdWithIncludes(
                p => p.ProductId == id,
                x => x.Images);

            if (product == null)
                throw new KeyNotFoundException("Product Not Found");

            if (!string.IsNullOrEmpty(product.Photo))
                _imgService.Delete(product.Photo);

            foreach (var img in product.Images)
                _imgService.Delete(img.ImageUrl);

            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
