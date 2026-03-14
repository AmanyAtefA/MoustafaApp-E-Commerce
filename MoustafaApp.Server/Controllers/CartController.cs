using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoustafaApp.Server.Dtos.CartDtos;
using MoustafaApp.Server.Dtos.OrderDtos;
using MoustafaApp.Server.Service.CartService.CartService;
using MoustafaApp.Server.Service.OrderService;
using System.Security.Claims;

namespace MoustafaApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, ICheckoutService checkoutService,
                                IMapper mapper)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
            _mapper = mapper;
        }


        [HttpGet("GetCartByUserId")]
        public async Task<IActionResult> GetCartsByUserId()
        {
            
            var cart = await _cartService.GetCartByUserId();

            return Ok(cart);
        }



       
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] AddItemDto dto)
        {

            var cart = await _cartService.AddItemToCart(dto);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }



        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityItemDto dto)
        {

            var cart = await _cartService.UpdateQuantity(dto);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }



        [HttpDelete("RemoveItem/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            
            var cart = await _cartService.RemoveItem(cartItemId);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }


        [HttpDelete("DeleteCart")]
        public async Task<IActionResult> DeleteCart()
        {

            var result = await _cartService.DeleteCart();

            if (!result)
                return NotFound();

            return NoContent();
        }




        [HttpPost("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon([FromBody] ApplyCouponDto dto)
        {
            var cart = await _cartService.ApplyCoupon(dto.Code);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }


        [HttpDelete("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {

            var cart = await _cartService.RemoveCoupon();

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }


        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] AddressDto dto)
        {
            var request = _mapper.Map<AddressDto>(dto);

            var orderId = await _checkoutService.CheckoutAsync(request);

            return Ok(new { orderId });
        }




        [Authorize]
        [HttpGet("test-auth")]
        public IActionResult Test()
        {
            var claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            });

            return Ok(claims);
        }

        [HttpGet("test-open")]
        public IActionResult TestOpen()
        {
            return Ok("API works");
        }
    }
}