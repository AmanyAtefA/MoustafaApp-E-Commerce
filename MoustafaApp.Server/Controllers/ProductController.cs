using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoustafaApp.Server.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImageService _imgService;

    public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imgService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imgService = imgService;
    }

    [HttpGet("GetAllProductsWithDetails")]
    public async Task<IActionResult> GetAllProductsWithDetails()
    {

        try
        {
            var Products = await _unitOfWork.Products.GetAllProductsWithDetails();

            var result = _mapper.Map<IEnumerable<ProductDto>>(Products);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }    



    [HttpGet("GetProductyByIdWithDetails/{id}")]
    public async Task<IActionResult> GetProductyByIdWithDetails(int id)
    {
        try {
            var Product = await _unitOfWork.Products.GetProductyByIdWithDetails(id);

            if (Product == null)
                return NotFound(new { message = "Product not found" });

            var result = _mapper.Map<ProductDto>(Product);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



    [HttpGet("GetAllProductsPagedinationaNewArrivals")]
    public async Task<ActionResult<PagedResult<ProductDto>>> GetAllProductsNewArrivals(int page = 1, int pageSize = 8)
    {
        var result = await _unitOfWork.Products
            .GetAllProductsNewArrivalsAsync(page, pageSize);

        return Ok(result);
    }




    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto)
    {
        try { 
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var Product = _mapper.Map<Product>(dto);
        Product.Images ??= new List<ProductImage>();


        if (dto.Photo != null)
        {
            Product.Photo = _imgService.Save(dto.Photo);
        }


        if (dto.Images != null)
        {
            foreach (var img in dto.Images)
            {
                if (img.Photo == null)
                    continue;
                var url = _imgService.Save(img.Photo);
                Product.Images.Add(new ProductImage { ImageUrl = url });
            }

        }

        await _unitOfWork.Products.AddAsync(Product);
        _unitOfWork.CommitChanges();

        return Ok(new { Message = "Product Created Successfully.", Product = Product });
    
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }




[HttpPut("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var OldProduct = await _unitOfWork.Products.GetByIdWithIncludes(
                p => p.ProductId == id,
                x => x.Images);

            if (OldProduct == null)
                return NotFound(new { message = "Product Not Found" });


            _mapper.Map(dto, OldProduct);


            if (dto.Photo != null)
            {
                if (!string.IsNullOrEmpty(OldProduct.Photo))
                    _imgService.Delete(OldProduct.Photo);

                OldProduct.Photo = _imgService.Save(dto.Photo);
            }


            _unitOfWork.Products.Update(OldProduct);
            _unitOfWork.CommitChanges();

            var UpdatedProduct = _mapper.Map<ProductDto>(OldProduct);
            return Ok(new { Message = "Product Updated Successfully.", UpdatedProduct });
        }

        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {

        try
        {
            var Product = await _unitOfWork.Products.GetByIdWithIncludes(
                p => p.ProductId == id,
                x => x.Images);

            if (Product == null)
                return NotFound(new { message = "Product Not Found" });

            if (!string.IsNullOrEmpty(Product.Photo))
                _imgService.Delete(Product.Photo);

            foreach (var img in Product.Images)
                _imgService.Delete(img.ImageUrl);

            _unitOfWork.Products.Delete(Product);
            _unitOfWork.CommitChanges();

            return Ok(new { message = "Product Deleted Successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
