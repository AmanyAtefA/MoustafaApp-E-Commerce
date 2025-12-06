


[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet("GetAllCarts")]
    public async Task<IActionResult> GetAllCarts()
    {
        try
        {
            var Carts = await _unitOfWork.Carts.GetAllCarts();
            var result = _mapper.Map<IEnumerable<CartDto>>(Carts);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }



    [HttpGet("GetCartById/{id}")]
    public async Task<IActionResult> GetCartById(int id)
    {
        try
        {
            var Cart = await _unitOfWork.Carts.GetCartById(id);
              

            if (Cart == null)
                return NotFound(new { message = "Cart Not Found" });

            var result = _mapper.Map<CartDto>(Cart);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }



    //[HttpPost("CreateCart")]
    //public async Task<IActionResult> CreateCartDto([FromBody] CreateCartDto dto)
    //{
        
    //    return Ok();

    //}




    [HttpDelete("DeleteCart/{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        try { 
        var Cart = await _unitOfWork.Carts.GetTById(id);

        if (Cart == null)
            return NotFound();

        _unitOfWork.Carts.Delete(Cart);
        _unitOfWork.CommitChanges();

            return Ok(new { message = "Carts Deleted Successfully" });

        }


        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
