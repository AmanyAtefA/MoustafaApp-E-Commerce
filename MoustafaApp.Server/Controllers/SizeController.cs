using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoustafaApp.Server.Dtos;
using MoustafaApp.Server.Dtos.ProductDtos;

namespace MoustafaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SizeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllSizes")]
        public async Task<IActionResult> GetAllSizes()
        {

            try
            {
                var Products = await _unitOfWork.Sizes.GetAllAsync();

                var result = _mapper.Map<IEnumerable<SizetDto>>(Products);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
