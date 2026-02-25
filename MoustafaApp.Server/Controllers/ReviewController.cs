

using Microsoft.AspNetCore.Authorization;
using MoustafaApp.Server.Dtos.Review;
using MoustafaApp.Server.Dtos.ReviewDtos;
using MoustafaApp.Server.Models;
using System.Security.Claims;

namespace MoustafaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
              var reviews = await _unitOfWork.Reviews.GetAllReviews();

              return Ok(reviews); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("GetReviewsByProductId/{id}")]
        public async Task<IActionResult> GetReviewsByProductId(int id)

        {
            try {
                var reviews = await _unitOfWork.Reviews.GetReviewsByProductId(id);

                return Ok(reviews);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [Authorize]
        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(/*[FromForm]*/ CreateReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          
            await _unitOfWork.Reviews.CreateReviewAsync(dto, userId!);

            return Ok(new { message = "Review added successfully" });
        }

        [Authorize]
        [HttpPut("UpdateReview/{id}")]
        public async Task<IActionResult> UpdateReview(int id, UpdateReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            var updated = await _unitOfWork.Reviews.UpdateReviewAsync(id, dto, userId!, isAdmin);

            if (!updated)
                return NotFound();

            return Ok(new { message = "Review updated successfully" });
        }


        [Authorize]
        [HttpDelete("DeleteReview/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            var Review = await _unitOfWork.Reviews.DeleteReviewAsync(id, userId!, isAdmin);

            if (!Review)
                return NotFound(new { message = "Review not Found" });

            return Ok(new { message = "Review deleted successfully" });
        }



        [HttpGet("GetProductReviews/{productId}")]
        public async Task<IActionResult> GetProductReviews(int productId,
                    [FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 5)
        {
            var reviews = await _unitOfWork.Reviews
                .GetPagedReviewsByProductId(productId, pageNumber, pageSize);

            var stats = await _unitOfWork.Reviews
                .GetReviewStatsByProductId(productId);

            return Ok(new ProductReviewsResponseDto
            {
                Stats = stats,
                Reviews = reviews
            });
        }


    }
}
