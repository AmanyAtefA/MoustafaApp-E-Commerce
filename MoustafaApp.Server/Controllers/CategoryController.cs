

using MoustafaApp.Server.Models;

namespace moustafapp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var Categories = await _unitOfWork.Categories.GetAllAsync();
                var result = _mapper.Map<IEnumerable<CategoryDto>>(Categories);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }



        [HttpGet("GetAllCategoriesWithProductsThenInclude")]
        public async Task<IActionResult> GetAllCategoriesWithProductsThenInclude()
        {
            try
            {
                var Categories = await _unitOfWork.Categories
                                .GetAllCategoriesWithProductsThenInclude();

                var result = _mapper.Map<IEnumerable<CategoryDto>>(Categories);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }




        [HttpGet("GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var Category = await _unitOfWork.Categories.GetTById(id);

                if (Category == null)
                    return NotFound(new { message = "Category Not Found" });

                var result = _mapper.Map<CategoryDto>(Category);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }




        [HttpGet("GetCategoryByIdWithProductsThenInclude/{id}")]
        public async Task<IActionResult> GetCategoryByIdWithProductsThenInclude(int id)
        {
            try
            {
                var Category = await _unitOfWork.Categories
                                .GetCategoryByIdWithProductsThenInclude(id);

                var result = _mapper.Map<CategoryDto>(Category);

                if (Category == null)
                    return NotFound(new { message = "Category Not Found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }




        [HttpGet("GetCategoryByIdWithProducts/{id}")]
        public async Task<IActionResult> GetByCategoryByIdWithProducts(int id)
        {
            try
            {
                var Category = await _unitOfWork.Categories
                    .GetByIdWithIncludes(
                        x => x.CategoryId == id,
                        y => y.Products
                    );

                if (Category == null)
                    return NotFound(new { message = "Category Not Found" });

                var result = _mapper.Map<CategoryDto>(Category);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }




        [HttpPost("CreateCategory")]

        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto dto)
        {
            try
            {
                var Category = _mapper.Map<Category>(dto);

                await _unitOfWork.Categories.AddAsync(Category);
                _unitOfWork.CommitChanges();

                return Ok(new
                {
                    message = "Category Added Successfully",
                    data = Category
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }




        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromForm] CreateCategoryDto dto)
        {
            try
            {
                var OldCategory = await _unitOfWork.Categories.GetTById(id);
                if (OldCategory == null)
                    return NotFound(new { message = "Category NotFound" });

                _mapper.Map(dto, OldCategory);
                _unitOfWork.Categories.Update(OldCategory);
                _unitOfWork.CommitChanges();

                return Ok(new
                {
                    message = "Category Updated Successfully",
                    data = OldCategory
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }




        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var Category = await _unitOfWork.Categories.GetTById(id);
                if (Category == null)
                    return NotFound(new { message = "Category Not Found" });

                _unitOfWork.Categories.Delete(Category);
                _unitOfWork.CommitChanges();

                return Ok(new { message = "Category Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
