

using moustafapp.Server.GenericOfWork;
using Nest;

namespace MoustafaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DipartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DipartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet ("GetAllDipartments")]
        public async Task<IActionResult> GetAllDipartments()
        {
            try
            { 
                   var Dipartments = await _unitOfWork.Departments.GetAllAsync();

                   var result = _mapper.Map<IEnumerable<DepartmentDto>>(Dipartments);
                   return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }



        [HttpGet ("GetAllDepartmentsWithProducts")]
        public async Task<IActionResult> GetAllDepartmentsWithProducts()
        {
            try
            { 
                   var Dipartments = await _unitOfWork.Departments.GetAllDepartmentsWithProducts();

                   var result = _mapper.Map<IEnumerable<DepartmentWithProductDto>>(Dipartments);
                   return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }



        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetTById(id);
                if (department == null)
                {
                    return NotFound();
                }
                var result = _mapper.Map<DepartmentDto>(department);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }



        [HttpGet("GetDepartmentByIdWithProducts/{id}")]

        public async Task<IActionResult> GetDepartmentByIdWithProducts(int id)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetDepartmentByIdWithProducts(id);
                if (department == null)
                {
                    return NotFound();
                }
                var result = _mapper.Map<DepartmentWithProductDto>(department);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var department =  await _unitOfWork.Departments.GetTById(id);
                if (department == null)
                {
                    return NotFound();
                }
                _unitOfWork.Departments.Delete(department);
                _unitOfWork.CommitChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
