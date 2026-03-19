
using Microsoft.AspNetCore.Authorization;
using MoustafaApp.Server.Service.OrderService;

namespace MoustafaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        private readonly IMapper _mapper;

        public OrderController( IOrderService OrderService,IMapper mapper)
        {
            _OrderService = OrderService;
            _mapper = mapper;
        }

        [HttpGet("GetOrderById/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _OrderService.GetOrderById(orderId);

            if (order == null)
                return NotFound();

            return Ok(order);
        }


        [HttpGet("GetOrderByUserId")]
        public async Task<IActionResult> GetOrderByUserId()
        {
            var order = await _OrderService.GetOrderByUserId();

            if (order == null)
                return NotFound();

            return Ok(order);
        }

    }
}

     