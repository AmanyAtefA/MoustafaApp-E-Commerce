using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoustafaApp.Server.Dtos.ProductDtos;
using MoustafaApp.Server.Models;
using MoustafaApp.Server.Service.ProductService;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImageService _imgService;
    private readonly IProductService _productService;
    public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imgService,
                                 IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imgService = imgService;
        _productService = productService;

    }

    [HttpGet("GetAllProductsWithDetails")]
    public async Task<IActionResult> GetAllProductsWithDetails()
    {

        try
        {
            var products = await _unitOfWork.Products.GetAllProductsWithDetails();
            return Ok(products);
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
            if (id <= 0)
                return BadRequest("Invalid product id");

            var product = await _unitOfWork.Products.GetProductByIdWithDetails(id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }




    [HttpGet("GetProductsWithFilter")]
    public async Task<IActionResult> GetProductsWithFilter([FromQuery] ProductFilterQueryDto FilterQuery)
    {
        var result = await _unitOfWork.Products.GetProductWithFiltersAsync(FilterQuery);
        return Ok(result);
    }

    [HttpGet("GetAllProductsNewArrivalsAsync")]
    public async Task<IActionResult> GetAllProductsNewArrivalsAsync(int page, int pageSize)
    {
        var result = await _unitOfWork.Products.GetAllProductsNewArrivalsAsync(page, pageSize);
        return Ok(result);
    }




    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _productService.CreateAsync(dto);
        return Ok(new { Message = "Product Created Successfully", Product = result });
    }



    [HttpPut("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _productService.UpdateAsync(id, dto);
        return Ok(new { Message = "Product Updated Successfully", Product = result });
    }



    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);
        return Ok(new { Message = "Product Deleted Successfully" });
    }



    //    [HttpPost("CreateProduct")]
    //    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto)
    //    {
    //        try { 

    //        if (!ModelState.IsValid)
    //            return BadRequest(ModelState);

    //        var Product = _mapper.Map<Product>(dto);
    //        Product.Images ??= new List<ProductImage>();


    //        if (dto.Photo != null)
    //        {
    //            Product.Photo = _imgService.Save(dto.Photo);
    //        }


    //        if (dto.Images != null)
    //        {
    //            foreach (var img in dto.Images)
    //            {
    //                if (img.Photo == null)
    //                    continue;
    //                var url = _imgService.Save(img.Photo);
    //                Product.Images.Add(new ProductImage { ImageUrl = url });
    //            }

    //        }

    //        await _unitOfWork.Products.AddAsync(Product);
    //        _unitOfWork.CommitChanges();

    //        return Ok(new { Message = "Product Created Successfully.", Product = Product });

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"Internal server error: {ex.Message}");
    //        }
    //    }




    //[HttpPut("UpdateProduct/{id}")]
    //    public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto dto)
    //    {
    //        try
    //        {
    //            if (!ModelState.IsValid)
    //                return BadRequest(ModelState);

    //            var OldProduct = await _unitOfWork.Products.GetByIdWithIncludes(
    //                p => p.ProductId == id,
    //                x => x.Images);

    //            if (OldProduct == null)
    //                return NotFound(new { message = "Product Not Found" });


    //            _mapper.Map(dto, OldProduct);


    //            if (dto.Photo != null)
    //            {
    //                if (!string.IsNullOrEmpty(OldProduct.Photo))
    //                    _imgService.Delete(OldProduct.Photo);

    //                OldProduct.Photo = _imgService.Save(dto.Photo);
    //            }


    //            _unitOfWork.Products.Update(OldProduct);
    //            _unitOfWork.CommitChanges();

    //            var UpdatedProduct = _mapper.Map<ProductDto>(OldProduct);
    //            return Ok(new { Message = "Product Updated Successfully.", UpdatedProduct });
    //        }

    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"Internal server error: {ex.Message}");
    //        }
    //    }



    //    [HttpDelete("DeleteProduct/{id}")]
    //    public async Task<IActionResult> DeleteProduct(int id)
    //    {

    //        try
    //        {
    //            var Product = await _unitOfWork.Products.GetByIdWithIncludes(
    //                p => p.ProductId == id,
    //                x => x.Images);

    //            if (Product == null)
    //                return NotFound(new { message = "Product Not Found" });

    //            if (!string.IsNullOrEmpty(Product.Photo))
    //                _imgService.Delete(Product.Photo);

    //            foreach (var img in Product.Images)
    //                _imgService.Delete(img.ImageUrl);

    //            _unitOfWork.Products.Delete(Product);
    //            _unitOfWork.CommitChanges();

    //            return Ok(new { message = "Product Deleted Successfully" });
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"Internal server error: {ex.Message}");
    //        }
    //    }


  

}
