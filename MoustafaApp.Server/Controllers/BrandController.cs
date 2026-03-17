using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoustafaApp.Server.Service.ProductService;

namespace MoustafaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
       
        public BrandController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

        [HttpGet("GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {

            try
            {
                var Brands = await _unitOfWork.Brands.GetAllBrands();
                return Ok(Brands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("GetBrandById/{id}")]
        public async Task<IActionResult> GetProductyByIdWithDetails(int id)
        {
            try
            {
               

                var Brand = await _unitOfWork.Brands.GetBrandById(id);

                if (Brand == null)
                    return NotFound("Brand not found");

                return Ok(Brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
